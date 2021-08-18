<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Selected Account" title-class="blue-light5--bg pa3" style="min-width:400px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="editFormValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <div style="text-align:left; border:solid 1px silver; padding:5px">
                    <div>
                        Account Id: {{ crmAccountData.id }}
                    </div>
                    <div>
                        Created: {{ crmAccountData.creationDateString }}
                    </div>
                    <div>
                        Created By: {{ crmAccountData.createdBy }}
                    </div>
                    <div>
                        Last Updated: {{ crmAccountData.lastUpdatedDateString }}
                    </div>
                    <div>
                        Updated By: {{ crmAccountData.lastUpdatedBy }}
                    </div>
                    <div>
                        Deleted: {{ crmAccountData.deletionDateString }}
                    </div>
                    <div>
                        Deleted By: {{ crmAccountData.deletionBy }}
                    </div>
                </div>
                <br />
                <div style="border:solid 1px silver; padding:5px">
                    <w-form
                        v-model="editFormValid"
                        @success="onEditSuccess"
                        >
                        <w-button
                            @click="detailCloseClick" 
                            sm 
                            outline 
                            round 
                            absolute 
                            icon="wi-cross" >
                        </w-button>
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
            </w-card>
        </w-flex>
        <br />
        <br />
        <w-flex  justify-center fill-width>
            <div>
                <account-member-list
                    :selectedAccountId="selectedAccountData.id"
                    >
                </account-member-list>
            </div>
        </w-flex>
    </div>
</template>

<script>
    import AccountMemberList from '../components/AccountMemberList.vue';

    export default {
        name:"CRMAccountDetail",
        components:{
            'account-member-list':AccountMemberList
        },
        data(){
            return {
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