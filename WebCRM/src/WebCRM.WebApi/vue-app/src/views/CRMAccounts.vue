<template>
    <div>
        <w-app block>
            <h1>Accounts</h1>
            <br />
            <crm-account-manager></crm-account-manager>
        </w-app>
    </div>
</template>

<script>
    import CRMAccountManager from '../components/CRMAccounts/CRMAccountManager.vue';

    export default {
        components: { 
            'crm-account-manager': CRMAccountManager,
        },
        name:"CRMAccounts",
    }
</script>

<style lang="scss" scoped>

</style>