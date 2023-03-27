<script lang="ts">
    export let title: string;
    export let value: any;
    export let group: any;
</script>

<div class="container">
    <label>
        <input type="radio" bind:group {value} />
        {title}
    </label>
</div>

<style>
    .container {
        display: flex;
        flex-direction: row;
        margin: 0px 10px;
    }

    label {
        margin: 0px;
        text-align: right;
        display: inline-block;
        cursor: pointer;
    }
</style>
