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
    const submitHandler = (event: any): void => {
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
            errors.educationMaxReached = "";
        }

        if (valid === false) return;
        createUser();

        event.target.reset();
    };

    let resetKey: boolean = false;
    const resetHandler = (): any => {
        resetKey = !resetKey;
    };

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isValidEmail = (email: string): boolean => {
        return emailRegex.test(email);
    };

    const createUser = () => {
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

    let resultStyle: string = "p-Ok";
    let resultText: string = "";
    const notify = (result: boolean): void => {
        if (result === true) {
            resultText = "Výsledek: OK";
            resultStyle = "p-OK";

            setTimeout(() => {
                resultText = "";
            }, 2000);
        } else {
            resultStyle = "p-Error";
            resultText = "Výsledek: Chyba";
        }
    };
</script>

<div class="form-container">
    <form on:submit={submitHandler} on:reset={resetHandler}>
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
                    {#key resetKey}
                        <UserEducationFormPart
                            bind:selectedValue={user.educationMaxReached}
                            displayErrorText={errors.educationMaxReached}
                        />
                    {/key}
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
            <UserInterestsFormPart bind:jsonValue={user.interests} />
        </div>

        <div style="display: flex; justify-content: space-between;">
            <div class="result-container">
                <p class={resultStyle}>{resultText}</p>
            </div>

            <div class="form-field-buttons">
                <button
                    type="reset"
                    class="full-button"
                    style="margin-right: 40px;">Znovu</button
                >
                <button type="submit" class="full-button">Vytvořit</button>
            </div>
        </div>
    </form>
</div>

<style>
    .form-container {
        display: flex;
        justify-content: center;
    }

    .result-container,
    .form-field-buttons,
    .form-field {
        display: block;
        margin: 15px 35px;
    }

    .form-field-buttons {
        display: flex;
        justify-content: end;
    }

    .form-field > input {
        display: block;
    }

    .result-container {
        text-align: center;
        align-items: center;
    }

    .p-OK {
        color: green;
    }

    .p-Error {
        color: rgb(183, 0, 0);
    }

    p {
        margin: 8px;
        font-weight: 500;
        padding: 0px;
    }
</style>
