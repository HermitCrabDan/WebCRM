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
                    @success="newAccountValidated"
                    >
                    <w-button 
                        @click="newAccountClose" 
                        sm 
                        outline 
                        round 
                        absolute 
                        icon="wi-cross" >
                    </w-button>
                    <div>
                         <w-input 
                            label="New Account Name" 
                            v-model="newAccount.newAccountName"
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
        props:{
            newAccount: Object,
        },
        emits:["newAccountValidated","newAccountClose"],
        data(){
            return {
                newAccountValid: null,
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
    }
</script>

<style lang="scss" scoped>

</style>