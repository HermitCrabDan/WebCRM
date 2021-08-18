<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="Edit Account" title-class="blue-light5--bg pa3" style="min-width:400px">
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
                    <w-button
                        @click="closeClick" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <w-input
                        label="Member Name"
                        v-model="crmAccountData.memberName"
                        :validators="[validators.required]"
                        >
                    </w-input>
                    <w-input
                        label="User Name"
                        placeholder="optional"
                        v-model="crmAccountData.memberName"
                        >
                    </w-input>
                    <w-button
                        class="my1"
                        type="submit"
                        >
                        Edit Member
                    </w-button>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    export default {
        name:"MemberDetail",
        props:{
            selectedMemberData: Object
        },
        emits:["memberDetailClose","memberEditSuccess"],
        data(){
            return{
                memberData: {},
                editFormValid: null,
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedMemberData:{
                immediate:true,
                deep:true,
                handler(newVal, oldVal){
                    console.log(newVal);
                    console.log(oldVal);
                    this.memberData = newVal;
                }
            }
        },
        methods:{
            closeClick(){
                this.$emit("memberDetailClose");
            },
            onEditSuccess(){
                this.$emit("memberEditSucess", this.memberData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>