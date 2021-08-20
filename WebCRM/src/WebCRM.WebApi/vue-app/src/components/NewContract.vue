<template>
    <div>

    </div>
</template>

<script>
    export default {
        name:"NewContract",
        props:{
            selectedMemberID: Number,
            selectedAccountID: Number,
        },
        data(){
            return{
                newContractData:{ accountID:0, memberID: 0 },
                newContractValid:null,

                availableMembers:[],
                availableAccounts:[],

                isLoading: false,
                isError: false,
                
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        emits:["newContractValidated","newContractClose"],
        methods:{
            closeClick(){
                this.$emit("newContractClose");
            },
            onValidationSuccess(){
                this.$emit("newContractValidated", this.newContractData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>