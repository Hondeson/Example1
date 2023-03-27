<script lang="ts">
    import { PUBLIC_API_PATH } from "$env/static/public";
    import type { User } from "../../types/User";
    import { onMount } from "svelte";

    const load = async (): Promise<User[]> => {
        const req = await fetch(
            PUBLIC_API_PATH + "api/Users?" + "offset=0" + "&limit=1000"
        );
        if (req.status === 204) {
            return [];
        }

        let data = await req.json();
        return data as User[];
    };

    let data: User[] = [];
    onMount(async () => {
        data = await load();
    });

    const toggleDropdown = (id: any) => {
        const dropdownContent = document.getElementById(
            `#dropdown-content-${id}`
        );

        dropdownContent?.classList.toggle("show");
    };
</script>

<div class="content">
    <table>
        <thead>
            <tr class="table-header">
                <th>Id</th>
                <th>Jméno a příjmení</th>
                <th>Email</th>
            </tr>
        </thead>
        <tbody>
            {#each data as user (user.id)}
                <tr class="table-row">
                    <td>{user.id}</td>
                    <td>{user.fullName}</td>
                    <td>{user.email}</td>
                </tr>
            {/each}
        </tbody>
    </table>
</div>

<style>
.content {
    height: 100%;
    overflow: auto;
}

table {
    border-collapse: collapse;
    width: 100%;
}

th {
    border-bottom: 1px solid var(--foreground-color);
    text-align: center;
    padding: 10px;
}

td {
    text-align: center;
    padding: 10px;
}

.table-header {
    user-select: none;
}

td:first-child,
th:first-child {
    text-align: center;
}

th:hover {
    background-color: inherit;
}

.table-row {
    border: 1px solid var(--foreground-color);
    border-left: none;
    border-right: none;
    position: relative;
}

.table-row:hover {
    background-color: var(--selected-color);
    cursor: pointer;
}
</style>
