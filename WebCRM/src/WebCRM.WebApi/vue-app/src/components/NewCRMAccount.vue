<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="New Account" title-class="blue-light5--bg pa3" style="min-width:400px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="newAccountValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-form
                    v-model="newAccountValid"
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
                            label="New Account Name" 
                            v-model="newAccountData.accountName"
                            :validators="[validators.required]"
                            >
                        </w-input>
                    </div>
                    <br />
                    <div>
                        <w-button type="submit">Submit Account</w-button>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    export default {
        name:"NewCRMAccount",
        emits:["newAccountValidated", "newAccountClose"],
        data(){
            return {
                newAccountValid: null,
                newAccountData: { accountName: '' },
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        methods:{
            closeClick(){
                this.$emit("newAccountClose");
            },
            validationSuccess(){
                this.$emit("newAccountValidated", this.newAccountData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>