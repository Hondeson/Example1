<script lang="ts">
    import Tabs from "../components/Tabs.svelte";
    import UserGridTab from "../components/tabs/UserGridTab.svelte";
    import CreateUserTab from "../components/tabs/CreateUserTab.svelte";

    let items = [
        { label: "Uživatelé", value: 1, component: UserGridTab },
        { label: "Vytvořit uživatele", value: 2, component: CreateUserTab },
    ];
</script>

<div class="content">
    <Tabs {items} />
</div>

<style>
    .content {
        margin: 0px 60px;
        height: 80%;
    }
</style>
