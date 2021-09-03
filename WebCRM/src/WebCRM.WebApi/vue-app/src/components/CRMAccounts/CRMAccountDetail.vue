<template>
    <div>
        <h3>
            Account:
        </h3>
        <br />
        <w-flex justify-center fill-width>
            <w-card title title-class="blue-light5--bg pa3" style="width:800px">
                <template #title>
                    <div class="title3">
                        Selected Account:  {{ crmAccountData.id }} - {{ crmAccountData.accountName }}
                    </div>
                </template>
                <w-button
                    @click="detailCloseClick" 
                    sm 
                    outline 
                    round 
                    absolute 
                    icon="wi-cross" >
                </w-button>
                <w-tabs :items="tabs" card>
                    <template #item-title.1>
                        Account Details
                    </template>
                    <template #item-content.1>
                        <model-details
                            :modelData="crmAccountData">
                        </model-details>
                        <br />
                        <w-flex justify-center fill-width>
                            <div style="border:solid 1px silver; padding:5px; min-width:400px">
                                <w-form
                                    v-model="editFormValid"
                                    @success="onEditSuccess"
                                    >
                                    <div class="message-box">
                                        <w-alert
                                            class="my0 text-light"
                                            v-if="editFormValid === false"
                                            error
                                            no-border>
                                            The form has errors.
                                        </w-alert>
                                    </div>
                                    <w-input
                                        label="Account Name"
                                        v-model="crmAccountData.accountName"
                                        :validators="[validators.required]"
                                        >
                                    </w-input>
                                    <w-button
                                        class="my1"
                                        type="submit"
                                        >
                                        Edit Account Name
                                    </w-button>
                                </w-form>
                            </div>
                        </w-flex>
                    </template>
                    <template #item-title.2>
                        Memberships
                    </template>
                    <template #item-content.2>
                        <w-flex  justify-center fill-width>
                            <div>
                                <account-membership-manager
                                    :selectedAccountId="selectedAccountData.id"
                                    >
                                </account-membership-manager>
                            </div>
                        </w-flex>
                    </template>
                </w-tabs>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import AccountMembershipManager from './AccountMemberships/AccountMembershipManager.vue';
    import ModelDetails from '../ModelDetails.vue';

    export default {
        name:"CRMAccountDetail",
        components:{
            'account-membership-manager':AccountMembershipManager,
            'model-details':ModelDetails,
        },
        data(){
            return {
                tabs: [
                    { title: 'Account Details', content: 'Account Details' },
                    { title: 'Memberships', content: 'Memberships' }
                ],
                editFormValid: null,
                crmAccountData: { accountName: '' },
                validators: {
                    required: value => !!value || 'This field is required',
                },

            }
        },
        props:{
            selectedAccountData: Object
        },
        watch:{
            selectedAccountData: {
                immediate: true,
                deep: true,
                handler(newVal,oldVal){
                    this.crmAccountData = newVal;
                    console.log(newVal);
                    console.log(oldVal);
                }
            }
        },
        emits: ["editValidationSuccess", "accountDetailClose" ],
        methods:{
            onEditSuccess(){
                this.$emit("editValidationSuccess", this.crmAccountData);
            },
            detailCloseClick(){
                this.$emit("accountDetailClose");
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>