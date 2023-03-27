<script lang="ts">
    import { Interests } from "../../../types/Interests";
    import { isNullOrWhitespace } from "../../../helper/helper";
    export let jsonValue: string;

    const data = [
        {
            value: Interests.Bike,
            label: "Cyklistika",
            selected: false,
        },
        {
            value: Interests.Fishing,
            label: "Rybaření",
            selected: false,
        },
        {
            value: Interests.StandingInWaitingQueue,
            label: "Stání ve frontách",
            selected: false,
        },
        {
            value: Interests.PcGaming,
            label: "Hraní PC her",
            selected: false,
        },
        { value: Interests.Other, label: "Jiné:", selected: false },
    ];
    let otherTextValue: string = "";

    const setResultString = (): void => {
        const selectedValues = data
            .filter((item) => item.selected)
            .map((item) => item.value);

        if (selectedValues.length === 0) {
            jsonValue = "";
            return;
        }

        const result = {
            value: selectedValues,
            otherSpec: otherTextValue,
        };

        jsonValue = JSON.stringify(result);
    };

    $: data[0].selected,
        data[1].selected,
        data[2].selected,
        data[3].selected,
        data[4].selected,
        setResultString();

    $: clearOther(!data[4].selected);

    const clearOther = (should: boolean) => {
        if (should) otherTextValue = "";
    };

    const resultObj = {
        val: [],
        otherSpec: "",
    };

    const deserializeJsonString = () => {
        if (isNullOrWhitespace(jsonValue)) return;

        const deserialized = JSON.parse(jsonValue);
        const selectedValues = deserialized.value;

        data.forEach((item) => {
            item.selected = selectedValues.includes(item.value);
        });

        otherTextValue = deserialized.otherSpec;
    };

    deserializeJsonString();
</script>

<label for="interests" class="top-label">Zájmy</label>
<div id="interests" class="horizontal-container">
    <div class="vertical-container">
        <label>
            <input type="checkbox" bind:checked={data[0].selected} />
            {data[0].label}
        </label>

        <label>
            <input type="checkbox" bind:checked={data[1].selected} />
            {data[1].label}
        </label>
    </div>

    <div class="vertical-container">
        <label>
            <input type="checkbox" bind:checked={data[2].selected} />
            {data[2].label}
        </label>

        <label>
            <input type="checkbox" bind:checked={data[3].selected} />
            {data[3].label}
        </label>
    </div>
</div>

<div style="margin-top: 20px;">
    <label style="display: inline-block;">
        <input
            type="checkbox"
            id="5"
            name="other"
            bind:checked={data[4].selected}
        />
        {data[4].label}
    </label>

    <textarea
        name="other-spec"
        rows="5"
        cols="40"
        disabled={!data[4].selected}
        bind:value={otherTextValue}
        on:focusout={() => setResultString()}
    />
</div>

<style>
    label {
        display: block;
        margin-top: 5px;
        cursor: pointer;
    }

    .top-label {
        display: block;
        margin-top: 5px;
        margin-bottom: 3px;
        cursor: default;
    }

    .horizontal-container {
        display: flex;
    }

    .vertical-container {
        display: block;
        margin-right: 60px;
    }

    textarea {
        width: 100%;
        padding: 6px;
    }
</style>
