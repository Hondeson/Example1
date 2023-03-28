<script lang="ts">
    import "../style.css";
    import ChangeThemeButton from "../components/ChangeThemeButton.svelte";
    import GitButton from "../components/GitButton.svelte";
</script>

<svelte:head>
    <title>Example1 User</title>
</svelte:head>

<header class="header-grid">
    <div class="grid-item">
        <ChangeThemeButton />
    </div>

    <div class="grid-item" style="justify-content: center;">
        <div class="title">
            <h1>Example 1</h1>
        </div>
    </div>

    <div class="grid-item" >
        <GitButton url="https://github.com/Hondeson/Ex1"/>
    </div>
</header>
<main class="content">
    <slot />
</main>

<style>
    .content {
        margin-top: calc(var(--header-height) * 2);
        height: calc(100vh - (var(--header-height) * 3));
    }

    :root {
        --header-height: 50px;
    }

    .header-grid {
        display: grid;
        grid-template-columns: 1fr 1fr 1fr;
        grid-template-rows: var(--header-height);
        position: fixed;
        top: 0px;
        left: 0px;
        right: 0;
        height: var(--header-height);
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
        z-index: 100;
        color: var(--foreground-color);
    }

    .grid-item {
        align-items: center;
        background-color: var(--content-color);
        display: flex;
    }

    .grid-item:nth-child(3) {
        justify-content: flex-end;
        padding-right: 16px;
    }

    .title {
        font-size: 18px;
        user-select: none;
        display: flex;
        justify-content: center;
    }

    h1 {
        text-align: center;
    }
</style>
