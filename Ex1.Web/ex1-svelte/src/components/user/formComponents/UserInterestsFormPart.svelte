<script lang="ts">
    let otherChecked: boolean = false;

    const data = [
        {
            value: 1,
            label: "Cyklistika",
            selected: false,
        },
        {
            value: 2,
            label: "Rybaření",
            selected: false,
        },
        { value: 3, label: "Stání ve frontách", selected: false },
        { value: 4, label: "Hraní PC her", selected: false },
        { value: 5, label: "Jiné", selected: false },
    ];

    let otherTextValue: string = "";
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
    <label>
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

    textarea{
        width: 100%;
    }
</style>
