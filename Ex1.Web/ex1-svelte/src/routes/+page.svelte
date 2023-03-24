<script lang="ts">
    import { PUBLIC_API_PATH } from "$env/static/public";
    import type { User } from "../types/User";
    import { onMount } from "svelte";

    const load = async (): Promise<User[]> => {
        const req = await fetch(PUBLIC_API_PATH + "api/Users");
        let data = await req.json();
        return data as User[];
    };

    let data: User[] = [];
    onMount(async () => {
        data = await load();
        console.log(data);
    });
</script>

<div class="header">
    <div class="header-mode">
        <a href="https://github.com/Hondeson/Ex1">Git</a>
    </div>
</div>

<div class="tabs-box">
    <div class="tab" class:active={true}>
        <span class="tab-label">ahoj</span>
    </div>
    <div class="tab" class:active={false}>
        <span class="tab-label">sadasdsa</span>
    </div>
</div>

<style>
.header{
    display: flex;
    justify-content: end;
}

    .tabs-box {
        display: flex;
        background-color: rebeccapurple;
    }

    .tab {
        display: inline-block;
        padding: 10px;
        cursor: pointer;
    }

    .tab.active {
        border-bottom: 2px solid blue;
    }

    .tab-label {
        font-weight: bold;
    }
</style>
