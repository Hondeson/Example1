<script lang="ts">
    import DiGithubBadge from "svelte-icons/di/DiGithubBadge.svelte";
    export let url: string = "";
</script>

<a href={url} target="_blank">
<div class="container">
    <p>Git</p>
    <div class="icon">
        
            <DiGithubBadge />
        
    </div>
</div>
</a>

<style>
    .container {
        display: flex;
        justify-content: center;
        align-items: center;
    }



    .icon {
        color: var(--foreground-color);
        width: 48px;
        height: 48px;
        margin-right: 20px;
        justify-content: flex-start;
        user-select: none;
        cursor: pointer;
    }

p{
    color: var(--foreground-color);
    font-weight: 600;
}

     a {
        color: transparent;
    }
</style>
