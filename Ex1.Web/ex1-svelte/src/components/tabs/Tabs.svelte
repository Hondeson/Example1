<script lang="ts">
    export let items: any[] = [];
    export let activeTabValue = 1;

    const handleClick = (tabValue: any) => () => (activeTabValue = tabValue);
    const handleKeyDown = (event: any, value: any) => {
        if (event.key === "Enter") {
            handleClick(value);
        }
    };
</script>

<ul>
    {#each items as item}
        <li class={activeTabValue === item.value ? "active" : ""}>
            <span
                style="user-select: none;"
                on:click={handleClick(item.value)}
                on:keydown={(event) => handleKeyDown(event, item.value)}
            >
                {item.label}
            </span>
        </li>
    {/each}
</ul>

{#each items as item}
    {#if activeTabValue == item.value}
        <div class="box">
            <svelte:component this={item.component} />
        </div>
    {/if}
{/each}

<style>
    .box {
        margin-bottom: 10px;
        padding: 20px 40px;
        border: 1px solid #dee2e6;
        border-radius: 0 0 0.5rem 0.5rem;
        border-top: 0;
        height: 100%;
        min-height: 600px;
        background-color: var(--content-color);
    }

    ul {
        display: flex;
        flex-wrap: wrap;
        padding-left: 0;
        margin-bottom: 0;
        list-style: none;
        border-bottom: 1px solid #dee2e6;
    }
    li {
        margin-bottom: -1px;
    }

    span {
        border: 1px solid transparent;
        border-top-left-radius: 0.25rem;
        border-top-right-radius: 0.25rem;
        display: block;
        padding: 0.5rem 1rem;
        cursor: pointer;
    }

    span:hover {
        border-color: #e9ecef #e9ecef #dee2e6;
    }

    li.active > span {
        color: var(--foreground-color);
        background-color: var(--selected-color);
        border-color: #dee2e6 #dee2e6 #fff;
    }
</style>
