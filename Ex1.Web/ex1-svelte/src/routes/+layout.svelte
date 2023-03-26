<script lang="ts">
    import "../style.css";
    import { Icon } from "@smui/common";
</script>

<svelte:head>
    <title>Example1 User</title>
</svelte:head>

<header class="header-grid">
    <div class="grid-item" />

    <div class="grid-item">
        <div class="title">
            <h1>Example 1</h1>
        </div>
    </div>

    <div class="grid-item">
        <!-- <div>
            <label for="lang" style="margin-right: 8px;">Jazyk:</label>
            <select name="lang" id="lang">
                <option value="cz">Česky &#x1F1E8;&#x1F1FF;</option>
                <option value="en">Anglicky</option>
            </select>
        </div>

        <div>
            <a href="https://github.com/Hondeson/Ex1">Git</a>
            <Icon class="material-icons">info</Icon>
        </div> -->
    </div>
</header>

<main class="content">
    <slot />
</main>

<style>
    .content {
        margin-top: var(--header-height);
        height: calc(100vh - (var(--header-height)*3));
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
        background-color: white;
        height: var(--header-height);
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
        z-index: 100;
        color: white;
    }

    .grid-item {
        justify-content: center;
        align-items: center;
        background-color: rgb(51, 63, 153);
        display: flex;
    }

    .grid-item:nth-child(3) {
        justify-content: flex-end;
        padding-right: 16px;
    }

    .title {
        font-size: 18px;
        user-select: none;
    }
</style>
