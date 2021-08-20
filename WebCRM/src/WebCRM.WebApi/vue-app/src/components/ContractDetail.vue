<template>
    <div>

    </div>
</template>

<script>
    export default {
        name:"ContractDetail",
        props:{
            selectedContractData: Object
        },
        emits:["contractDetailClose","contractEditValid"],
        data(){
            return{
                contractData:{},
                editContractValid:null,
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedContractData:{
                immediate:true,
                deep:true,
                handler(newVal, oldVal){
                    this.contractData = newVal;
                    console.log(oldVal);
                }
            }
        },
        methods:{
            closeClick(){
                this.$emit("contractDetailClose");
            },
            onValidationSuccess(){
                this.$emit("contractEditValid", this.contractData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>