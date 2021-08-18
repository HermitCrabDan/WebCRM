<template>
    <div>
        <w-notification v-model="isError" error bg-color="white">
            An Error Occured
        </w-notification>
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
                        Account Id: {{ accountMembershipData.accountID }}
                    </div>
                    <w-checkbox
                        v-model="accountMembershipData.isPrimaryAccountMember">
                        Is Primary Membership
                    </w-checkbox>
                    <w-select
                        label="Member"
                        v-model="accountMembershipData.memberID"
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
            existingMembershipIds: Array,
        },
        data(){
            return{
                newMembershipValid:null,
                accountMembershipData:{ 
                    accountID: 0, 
                    memberID: 0, 
                    isPrimaryAccountMember: false 
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
                    this.accountMembershipData.accountID = newVal;
                    this.loadMemberships();
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
                        this.updateAvailabeAccounts();
                    })
                    .catch(error => { 
                        console.log(error);
                        this.isError = true;
                    })
                    .then(()=>{this.isLoading = false;});
            },
            updateAvailabeAccounts(){
                const filteredMemberList = [];
                this.memberDataList.forEach(element => {
                    if(!this.existingMembershipIds.includes(element.id)){
                        filteredMemberList.push({ label:element.memberName, value: element.id})
                    }
                });
                this.availableMemberList = filteredMemberList;
                console.log(this.availableMemberList);
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>