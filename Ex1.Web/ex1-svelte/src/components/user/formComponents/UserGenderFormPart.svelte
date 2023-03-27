<script lang="ts">
    import { Gender } from "../../../types/Gender";
    import RadioSelect from "../../RadioSelect.svelte";

    export let displayErrorText: string = "";
    export let selectedValue: Gender;

    let selectedGender: Gender | null = null;
    const genderList = [
        { label: "Muž", value: Gender.Male },
        { label: "Žena", value: Gender.Female },
        { label: "Jiné", value: Gender.Other },
    ];

    const onGenderSelected = () => {
        selectedValue = selectedGender ?? Gender.NotSpecified;
    };

    $: selectedGender, onGenderSelected();
</script>

<label for="gender">Pohlaví</label>
<div class="container">
    {#each genderList as gender}
        <RadioSelect
            value={gender.value}
            title={gender.label}
            bind:group={selectedGender}
        />
    {/each}
</div>
<div class="form-error-field">{displayErrorText}</div>

<style>
    .container {
        display: flex;
        margin-top: 8px;
    }
</style>
