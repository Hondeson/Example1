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
</script>

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
<div class="form-error-field">
    {displayErrorText}
</div>

<style>
    .select-container {
        --border: 1px solid black;
        --border-hover: 1px solid black;
        --border-focused: 1px solid black;
        --border-radius: 3px;
        cursor: pointer;
    }
</style>
