<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="New Account Membership" title-class="blue-light5--bg pa3" style="min-width:400px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="newMembershipValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-form
                    v-model="newMembershipValid"
                    @success="onValidationSuccess">
                    <w-button
                        @click="closeBtnClick" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <div>
                        Account Id: {{ accountMembershipData.accountId }}
                    </div>
                    <w-checkbox
                        v-model="accountMembershipData.isPrimaryMembership">
                        Is Primary Membership
                    </w-checkbox>
                    <w-select
                        label="Member"
                        v-model="accountMembershipData.memberId"
                        :items="availableMemberList"
                        :validators="[validators.required]"
                        >
                    </w-select>
                    <w-button
                        class="my1"
                        type="submit"
                        >
                        Create Membership
                    </w-button>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name:"NewAccountMembership",
        emits:["newMembershipSuccess","newMembershipClose"],
        props:{
            selectedAccountId: Number,
            existingMemberships: Array,
        },
        data(){
            return{
                newMembershipValid:null,
                accountMembershipData:{ 
                    accountId: 0, 
                    memberId: null, 
                    isPrimaryMembership: false 
                },
                memberDataList:[],
                availableMemberList: [],
                isLoading: false,
                isError: false,
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedAccountId:{
                immediate: true,
                deep: true,
                handler(newVal, oldVal){
                    console.log(newVal);
                    console.log(oldVal);
                    this.accountMembershipData.accountId = newVal;
                    loadMemberships();
                }
            }
        },
        methods:{
            closeBtnClick(){
                this.$emit("newMembershipClose");
            },
            onValidationSuccess(){
                this.$emit("newMembershipSuccess", this.accountMembershipData);
            },
            loadMemberships(){
                this.isError = false;
                this.isLoading = true;
                axios
                    .get("api/MemberData")
                    .then(response => { 
                        this.memberDataList = response.data;
                        console.log(response.data);
                    })
                    .catch(error => { 
                        console.log(error);
                        this.isError = true;
                    })
                    .then(()=>{this.isLoading = false;});
            },
            updateAvailabeAccounts(){
                
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>