<script lang="ts">
    import { DateInput, localeFromDateFnsLocale } from "date-picker-svelte";
    import cs from "date-fns/locale/cs/index";
    import type { DateOnly } from "../../../types/DateOnly";

    export let selectedValue: DateOnly;
    export let displayErrorText: string = "";

    let locale = localeFromDateFnsLocale(cs);

    let selectedDate: Date | undefined | null = null;
    const onSelectedDate = () => {
        if (selectedDate === undefined || selectedDate == null) return;

        selectedValue = {
            year: selectedDate.getFullYear(),
            month: selectedDate.getMonth(),
            day: selectedDate.getDate(),
        };
    };

    $: selectedDate, onSelectedDate();

    const labelText = "Datum narození";
</script>

<label for="bornDate">
    {labelText}
    <DateInput
        bind:value={selectedDate}
        format="dd.MM.yyyy"
        placeholder=""
        {locale}
    />
</label>

<div class="form-error-field">{displayErrorText}</div>

<style>
</style>
