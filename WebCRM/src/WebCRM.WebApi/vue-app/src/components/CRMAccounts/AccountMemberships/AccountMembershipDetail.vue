<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Selected Membership" title-class="blue-light5--bg pa3" >
                <w-button
                    @click="closeClick" 
                    sm 
                    outline 
                    round 
                    absolute 
                    icon="wi-cross" >
                </w-button>
                <model-details
                    :modelData="selectedAccountMembership"
                    >
                </model-details>
                <div style="min-width:600px; text-align:left; border:solid 1px silver; padding:5px">
                    <div>
                        Membership Id: {{ selectedAccountMembership.id }}
                    </div>
                    <div>
                        Account Id: {{ selectedAccountMembership.accountID }}
                    </div>
                    <div>
                        Member Name: {{ selectedAccountMembership.memberName }}
                    </div>
                    <div>
                        Member ID: {{ selectedAccountMembership.memberID }}
                    </div>
                    <div>
                        Primary Account Membership: {{ selectedAccountMembership.isPrimaryAccountMemberString }}
                    </div>
                </div>
                <br />
                <div v-if="selectedAccountMembership.deletionDate">
                    <w-button @click="unDeleteMembershipClick">Reinstate Account Membership</w-button>
                </div>
                <div v-else>
                    <w-button @click="removeMembershipClick">Remove Account Membership</w-button>
                </div>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    import ModelDetails from '../../ModelDetails.vue';

    export default {
        name:"AccountMembershipDetail",
        components:{
            'model-details':ModelDetails,
        },
        emits:["removeMembership","unDeleteMembership","closeMembershipDetail"],
        props:{
            SelectedAccountMembershipData:Object
        },
        data(){
            return{
                selectedAccountMembership:{}
            }
        },
        methods:{
            removeMembershipClick(){
                this.$emit("removeMembership", this.selectedAccountMembership);
            },
            unDeleteMembershipClick(){
                this.$emit("unDeleteMembership",this.selectedAccountMembership);
            },
            closeClick(){
                this.$emit("closeMembershipDetail");
            }
        },
        watch:{
            SelectedAccountMembershipData:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.selectedAccountMembership = newVal;
                }
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>