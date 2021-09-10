<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title title-class="blue-light5--bg pa3" style="min-width:800px">
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
                                    <br />
                                    <div v-if="crmAccountData.deletionDate">
                                        <w-button @click="unDeleteClick">Reinstate Account</w-button>
                                    </div>
                                    <div v-else>
                                        <div>
                                            <w-button class="my1"
                                                type="submit">
                                                Edit Account
                                            </w-button>
                                        </div>
                                        <br />
                                        <div>
                                            <w-button @click="removeClick">Remove Account</w-button>
                                        </div>
                                    </div>
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
                                    :selectedAccountId="crmAccountData.id"
                                    >
                                </account-membership-manager>
                            </div>
                        </w-flex>
                    </template>
                    <template #item-title.3>
                        Notes
                    </template>
                    <template #item-content.3>
                        <w-flex justify-center fill-width>
                            <div>
                                <account-note-manager
                                    :selectedAccountId="crmAccountData.id"
                                    >
                                </account-note-manager>
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
    import AccountNoteManager from './AccountNotes/AccountNoteManager.vue';
    import ModelDetails from '../ModelDetails.vue';

    export default {
        name:"CRMAccountDetail",
        components:{
            'account-membership-manager':AccountMembershipManager,
            'account-note-manager':AccountNoteManager,
            'model-details':ModelDetails,
        },
        data(){
            return {
                tabs: [
                    { title: 'Account Details', content: 'Account Details' },
                    { title: 'Memberships', content: 'Memberships' },
                    { title: 'Notes', content:'Notes' },
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
                handler(newVal){
                    this.crmAccountData = newVal;
                }
            }
        },
        emits: ["editValidationSuccess", "accountDetailClose","removeAccountClick","reinstateAccountClick" ],
        methods:{
            onEditSuccess(){
                this.$emit("editValidationSuccess", this.crmAccountData);
            },
            detailCloseClick(){
                this.$emit("accountDetailClose");
            },
            removeClick(){
                this.$emit("removeAccountClick", this.crmAccountData);
            },
            unDeleteClick(){
                this.$emit("reinstateAccountClick", this.crmAccountData);
            },
        }
    }
</script>

<style lang="scss" scoped>

</style>