<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title title-class="blue-light5--bg pa3" style="min-width:800px">
                <template #title>
                    <div class="title3">
                        Selected Member: {{ memberData.id }} - {{ memberData.memberName }}
                    </div>
                </template>
                <w-button
                    @click="closeClick" 
                    sm 
                    outline 
                    round 
                    absolute 
                    icon="wi-cross" >
                </w-button>
                <w-tabs :items="tabs" card>
                    <template #item-title.1>
                        Member Details
                    </template>
                    <template #item-content.1>
                        <model-details :modelData="memberData">
                        </model-details>
                        <br />
                        <div style="min-width:600px; border:solid 1px silver; padding:10px">
                            <div class="message-box">
                                <w-alert
                                    class="my0 text-light"
                                    v-if="editFormValid === false"
                                    error
                                    no-border>
                                    The form has errors.
                                </w-alert>
                            </div>
                            <w-form
                                v-model="editFormValid"
                                @success="onEditSuccess"
                                >
                                <w-input
                                    label="Member Name"
                                    v-model="memberData.memberName"
                                    :validators="[validators.required]"
                                    >
                                </w-input>
                                <br />
                                <w-input
                                    label="User Name"
                                    placeholder="optional"
                                    v-model="memberData.userId"
                                    >
                                </w-input>
                                <br />
                                <div v-if="memberData.deletionDate">
                                    <w-button @click="unDeleteClick">Reinstate Member</w-button>
                                </div>
                                <div v-else>
                                    <div>
                                        <w-button class="my1"
                                            type="submit">
                                            Edit Member
                                        </w-button>
                                    </div>
                                    <br />
                                    <div>
                                        <w-button @click="removeClick">Remove Member</w-button>
                                    </div>
                                </div>
                            </w-form>
                        </div>
                    </template>
                    <template #item-title.2>
                        Member Contracts
                    </template>
                    <template #item-content.2>
                        <w-flex justify-center fill-width>
                            <div style="min-width:600px">
                                <member-contract-manager
                                    :selectedMemberId="memberData.id"
                                    :selectedMemberName="memberData.memberName"
                                    >
                                </member-contract-manager>
                            </div>
                        </w-flex>
                    </template>
                </w-tabs>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import ModelDetails from '../ModelDetails.vue';
    import MemberContractManager from './MemberContracts/MemberContractManager.vue';
    export default {
        components: { 
            'model-details':ModelDetails,
            'member-contract-manager':MemberContractManager,
        },
        name:"MemberDetail",
        props:{
            selectedMemberData: Object
        },
        emits:["memberDetailClose","memberEditSuccess","removeMemberClick","reinstateMemberClick"],
        data(){
            return{
                memberData: {},
                editFormValid: null,
                tabs:[
                    { title:'Edit Member', content:'Edit Member' },
                    { title:'Member Contracts', content:'Member Contracts' }
                ],
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedMemberData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.memberData = newVal;
                }
            }
        },
        methods:{
            closeClick(){
                this.$emit("memberDetailClose");
            },
            onEditSuccess(){
                this.$emit("memberEditSuccess", this.memberData);
            },
            removeClick(){
                this.$emit("removeMemberClick", this.memberData);
            },
            unDeleteClick(){
                this.$emit("reinstateMemberClick", this.memberData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>