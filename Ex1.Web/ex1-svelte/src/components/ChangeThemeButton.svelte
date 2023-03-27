<script lang="ts">
    import { onMount } from "svelte";
    import WiDaySunny from "svelte-icons/wi/WiDaySunny.svelte";
    import WiMoonAltWaningCrescent4 from "svelte-icons/wi/WiMoonAltWaningCrescent4.svelte";

    const toggleTheme = (): void => {
        window.document.body.classList.toggle("dark-mode");
        isDarkModeActive = !isDarkModeActive;
    };
    const onKeyDown = (event: KeyboardEvent): void => {
        if (event.key === "Enter") toggleTheme();
    };

    let isDarkModeActive: boolean;
    const checkDarkModeActive = (): boolean => {
        if (
            window.matchMedia &&
            window.matchMedia("(prefers-color-scheme: dark)").matches
        ) {
            return true;
        } else {
            return false;
        }
    };

    onMount(() => {
        isDarkModeActive = checkDarkModeActive();

        if (isDarkModeActive)
            window.document.body.classList.toggle("dark-mode");
    });
</script>

<div on:click={toggleTheme} on:keydown={onKeyDown} class="icon">
    {#if isDarkModeActive}
        <WiMoonAltWaningCrescent4 />
    {:else}
        <WiDaySunny />
    {/if}
</div>

<style>
    .icon {
        color: var(--foreground-color);
        width: 48px;
        height: 48px;
        margin: 0px 20px;
        justify-content: flex-start;
        user-select: none;
        cursor: pointer;
    }
</style>
