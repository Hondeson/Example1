<script lang="ts">
    import type { User } from "../types/User";
    import { getNewUser } from "../types/User";
    import { Gender } from "../types/Gender";
    import { Education } from "../types/Education";
    import { DateInput, localeFromDateFnsLocale } from "date-picker-svelte";
    import { cs } from "date-fns/locale";
    import { isNullOrWhitespace } from "../helper/helper";
    import { dateOnlyHasValue } from "../types/DateOnly";
    import Select from "svelte-select";
    import RadioSelect from "./RadioSelect.svelte";

    let locale = localeFromDateFnsLocale(cs);

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

        if (isNullOrWhitespace(user.educationMaxReached)) {
            valid = false;
            errors.educationMaxReached = "Zvolte maximální dosažené vzdělání!";
        } else {
            errors.educationMaxReached = "Zvolte datum narození!";
        }
    };

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isValidEmail = (email: string): boolean => {
        return emailRegex.test(email);
    };

    let selectedDate: Date | undefined | null = null;
    const onSelectedDate = () => {
        if (selectedDate === undefined || selectedDate == null) return;

        user.bornDate = {
            year: selectedDate.getFullYear(),
            month: selectedDate.getMonth(),
            day: selectedDate.getDate(),
        };
    };

    $: selectedDate, onSelectedDate();

    let selectedGender: Gender | null = null;
    const genderList = [
        { label: "Muž", value: Gender.Male },
        { label: "Žena", value: Gender.Female },
        { label: "Jiné", value: Gender.Other },
    ];

    const onGenderSelected = () => {
        user.gender = selectedGender ?? Gender.NotSpecified;
    };

    $: selectedGender, onGenderSelected();

    const educationList = [
        { label: "Bez formálního vzdělání", value: Education.NoFormal },
        { label: "Základní vzdělání", value: Education.Primary },
        { label: "Střední vzdělání", value: Education.Secondary },
        { label: "Bakalářské vzdělání", value: Education.Bachelors },
        { label: "Magisterské vzdělání", value: Education.Masters },
        {
            label: "Doktorát nebo vyšší vzdělání",
            value: Education.DoctorateOrHigher,
        },
    ];

    const onEducationSelected = (event: any) => {
        user.educationMaxReached = event.detail.value;
    };
</script>

<div class="form-container">
    <form on:submit|preventDefault={submitHandler}>
        <div style="display: flex;">
            <div style="display: block;">
                <div class="form-field">
                    <label for="name">Jméno a příjmení</label>
                    <input type="text" id="name" bind:value={user.fullName} />
                    <div class="error">{errors.fullname}</div>
                </div>

                <div class="form-field">
                    <label for="email">Email</label>
                    <input type="text" id="email" bind:value={user.email} />
                    <div class="error">{errors.email}</div>
                </div>
            </div>

            <div style="display: block;">
                <div class="form-field">
                    <label for="bornDate">Datum narození</label>
                    <DateInput
                        bind:value={selectedDate}
                        format="dd.MM.yyyy"
                        placeholder=""
                        {locale}
                    />
                    <div class="error">{errors.bornDate}</div>
                </div>

                <div class="form-field">
                    <label for="education">Maximální dosažené vzdělání</label>
                    <div class="select-container">
                        <Select
                            id="education"
                            name="education"
                            placeholder=""
                            items={educationList}
                            on:select={onEducationSelected}
                        />
                    </div>
                    <div class="error">{errors.educationMaxReached}</div>
                </div>
            </div>
        </div>

        <div class="form-field">
            <label for="gender">Pohlaví</label>
            <div style="display: flex; margin-top: 8px;">
                {#each genderList as gender}
                    <RadioSelect
                        name="gender"
                        value={gender.value}
                        title={gender.label}
                        bind:group={selectedGender}
                    />
                {/each}
            </div>
            <div class="error">{errors.gender}</div>
        </div>

        <div class="form-field">
            <label for="interests">Zájmy</label>
            <div id="interests" style="display: flex; justify-content: space-between;">
                <div>
                    <label>
                        <input type="checkbox" id="1" name="interests" />
                        Langoše
                    </label>

                    <label>
                        <input type="checkbox" id="2" name="interests" />
                        Hraní PC her
                    </label>
                </div>

                <div>
                    <label>
                        <input type="checkbox" id="3" name="interests" />
                        Langoše
                    </label>

                    <label>
                        <input type="checkbox" id="4" name="interests" />
                        Hraní PC her
                    </label>
                </div>
            </div>

            <div>
                <label>
                    <input type="checkbox" id="5" name="other" />
                    Jiné:
                </label>

                <textarea name="other-spec" rows="4" cols="40" />
            </div>
        </div>

        <button type="submit" class="full-button">Vytvořit</button>
    </form>
</div>

<style>
    .form-container {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .select-container {
        --border: 1px solid black;
        --border-hover: 1px solid black;
        --border-focused: 1px solid black;
        --border-radius: 3px;
        cursor: pointer;
    }

    .form-field {
        display: block;
        margin: 0 35px;
    }

    label {
        margin: 10px auto;
        text-align: left;
        display: block;
    }

    .error {
        text-align: center;
        justify-content: center;
        font-weight: bold;
        font-size: 16px;
        color: rgb(160, 0, 0);
        margin-top: 3px;
    }
</style>
