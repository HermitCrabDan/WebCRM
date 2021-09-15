<template>
    <div>
        <w-app block>
            <h1>Remittance</h1>
            <br />
            <remittance-manager></remittance-manager>
        </w-app>
    </div>
</template>

<script>
    import RemittanceManager from '../components/Remittance/RemittanceManager.vue';

    export default {
        name:"Remittance",
        components:{
            'remittance-manager':RemittanceManager
        },
    }
</script>

<style lang="scss" scoped>

</style>