<script lang="ts">
    import type { User } from "../../types/User";
    import { getNewUser } from "../../types/User";
    import { Gender } from "../../types/Gender";
    import { dateOnlyHasValue } from "../../types/DateOnly";
    import UserInterestsFormPart from "./formComponents/UserInterestsFormPart.svelte";
    import UserGenderFormPart from "./formComponents/UserGenderFormPart.svelte";
    import UserEducationFormPart from "./formComponents/UserEducationFormPart.svelte";
    import UserBornDateFormPart from "./formComponents/UserBornDateFormPart.svelte";
    import { Education } from "../../types/Education";
    import { PUBLIC_API_PATH } from "$env/static/public";

    let user: User = getNewUser();
    let errors = {
        fullname: "",
        email: "",
        bornDate: "",
        gender: "",
        educationMaxReached: "",
    };
    let valid: boolean = false;

    const nameRegex = /^(\w+\s){1,}\w+$/;
    const submitHandler = (): void => {
        valid = true;

        if (!nameRegex.test(user.fullName)) {
            valid = false;
            errors.fullname = "Jméno musí obsahovat aspoň 2 slova!";
        } else {
            errors.fullname = "";
        }

        if (!isValidEmail(user.email)) {
            valid = false;
            errors.email = "Neplatná emailová adresa!";
        } else {
            errors.email = "";
        }

        if (!dateOnlyHasValue(user.bornDate)) {
            valid = false;
            errors.bornDate = "Zvolte datum narození!";
        } else {
            errors.bornDate = "";
        }

        if (user.gender === Gender.NotSpecified) {
            valid = false;
            errors.gender = "Vyberte pohlaví!";
        } else {
            errors.gender = "";
        }

        if (user.educationMaxReached === Education.NotSpecified) {
            valid = false;
            errors.educationMaxReached = "Zvolte maximální dosažené vzdělání!";
        } else {
            errors.educationMaxReached = "Zvolte datum narození!";
        }

        if (valid === true) createUser();
    };

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isValidEmail = (email: string): boolean => {
        return emailRegex.test(email);
    };

    const createUser = () => {
        console.log("d");

        fetch(PUBLIC_API_PATH + "api/Users", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(user),
        }).then((response) => {
            if (response.status.toString().startsWith("20")) {
                notify(true);
                return;
            }

            notify(false);
        });
    };

    const notify = (result: boolean): void => {};
</script>

<div class="form-container">
    <form on:submit|preventDefault={submitHandler}>
        <div style="display: flex;">
            <div style="display: block;">
                <div class="form-field">
                    <label for="name">Jméno a příjmení</label>
                    <input type="text" id="name" bind:value={user.fullName} />
                    <div class="form-error-field">{errors.fullname}</div>
                </div>

                <div class="form-field">
                    <label for="email">Email</label>
                    <input type="text" id="email" bind:value={user.email} />
                    <div class="form-error-field">{errors.email}</div>
                </div>
            </div>

            <div style="display: block;">
                <div class="form-field">
                    <UserBornDateFormPart
                        bind:selectedValue={user.bornDate}
                        displayErrorText={errors.bornDate}
                    />
                </div>

                <div class="form-field">
                    <UserEducationFormPart
                        bind:selectedValue={user.educationMaxReached}
                        displayErrorText={errors.educationMaxReached}
                    />
                </div>
            </div>
        </div>

        <div class="form-field">
            <UserGenderFormPart
                bind:selectedValue={user.gender}
                displayErrorText={errors.gender}
            />
        </div>

        <div class="form-field interests-container">
            <UserInterestsFormPart />
        </div>

        <div style="display: flex; justify-content: end;  margin-top: 30px;">
            <button
                type="button"
                class="full-button"
                style="margin-right: 40px;">Smazat</button
            >
            <button type="submit" class="full-button">Vytvořit</button>
        </div>
    </form>
</div>

<style>
    .form-container {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .form-field {
        display: block;
        margin: 15px 35px;
    }

    label {
        margin: 10px auto;
        text-align: left;
        display: block;
    }
</style>
