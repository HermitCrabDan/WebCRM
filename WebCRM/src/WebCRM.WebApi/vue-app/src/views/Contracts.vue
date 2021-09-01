<template>
    <div>
        <w-app block>
            <h1>Contracts</h1>
            <br />
            <contract-manager></contract-manager>
        </w-app>
    </div>
</template>

<script>
import ContractManager from '../components/Contracts/ContractManager.vue';

export default {
    name:"Contracts",
    components:{
        'contract-manager':ContractManager,
    },
}
</script>

<style lang="scss" scoped>

</style>