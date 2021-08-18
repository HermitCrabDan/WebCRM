<template>
    <div>
        <w-button>
            Create New Account Membership
        </w-button>
        <w-table
            :headers="accountMemberHeaders"
            :items="crmAccountMemberList"
            :selectable-rows="1"
            @row-select="selectAccountMembership($event.item)">
        </w-table>
    </div>
</template>

<script>
    export default {
        name:"AccountMemberList",
        data(){
            return{
                crmAccountMemberList:[],
                accountMemberHeaders:[
                    { label:'Account ID', key:'accountId', align:'left' },
                    { label:'Member ID', key:'memberId', align:'left' },
                    { label:'Created by', key:'createdBy', align:'left' },
                    { label:'Date Entered', key:'creationDateString', align:'left' },
                    { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                    { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
                ],
                selectedAccountMembership: {}
            }
        },
        props:{
            accountId: Number,
            accountMemberListData: Array,
        },
        watch:{
            accountMemberListData:{
                immediate:true,
                deep: true,
                handler(newVal,oldVal){
                    this.crmAccountMemberList = newVal,
                    console.log(oldVal);
                }
            }
        },
        methods:{
            selectAccountMembership(accountMembership){
                this.selectAccountMembership = accountMembership;
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>