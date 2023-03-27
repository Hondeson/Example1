<script lang="ts">
    import Select from "svelte-select";
    import { Education } from "../../../types/Education";

    export let selectedValue: Education;
    export let displayErrorText: string = "";

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
        selectedValue = event.detail.value;
    };

    const labelText = "Maximální dosažené vzdělání";
</script>

<label for="education">
    {labelText}
    <div class="select-container">
        <Select
            id="education"
            name="education"
            placeholder=""
            items={educationList}
            on:select={onEducationSelected}
        />
    </div>
</label>

<div class="form-error-field">
    {displayErrorText}
</div>

<style>
    .select-container {
        --border: 1px solid var(--foreground-color);
        --border-hover: 1px solid var(--foreground-color);
        --border-focused: 1px solid var(--foreground-color);
        --border-radius: 3px;
        --background: var(--input-background);
        --list-background: var(--background-color);
        --item-hover-bg: var(--selected-color);
        cursor: pointer;
        --height: 36px;
    }
</style>
