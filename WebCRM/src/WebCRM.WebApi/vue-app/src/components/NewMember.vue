<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="New Member" title-class="blue-light5--bg pa3" style="min-width:400px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="newMemberValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-form
                    v-model="newMemberValid"
                    @success="validationSuccess"
                    >
                    <w-button 
                        @click="closeClick" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <div>
                         <w-input 
                            label="New Member Name" 
                            v-model="newMemberData.memberName"
                            :validators="[validators.required]"
                            >
                        </w-input>
                    </div>
                    <div>
                         <w-input 
                            label="User Name" 
                            placeholder="optional"
                            v-model="newMemberData.userId"
                            >
                        </w-input>
                    </div>
                    <br />
                    <div>
                        <w-button type="submit">Submit Member</w-button>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    export default {
        name:"NewMember",
        emits:["newMemberClose", "newMemberSuccess"],
        data(){
            return{
                newMemberValid: null,
                newMemberData: { memberName: '', userId: null },
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        methods:{
            closeClick(){
                this.$emit("newMemberClose");
            },
            validationSuccess(){
                this.$emit("newMemberSuccess", this.newMemberData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>