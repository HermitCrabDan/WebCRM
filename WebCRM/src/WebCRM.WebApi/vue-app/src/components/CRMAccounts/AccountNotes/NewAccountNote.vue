<template>
    <div>
        <w-flex justify-center fill-width>
            <w-card title="New Note" title-class="blue-light5--bg pa3" style="min-width:400px">
                <div class="message-box">
                    <w-alert
                        class="my0 text-light"
                        v-if="newNoteValid === false"
                        error
                        no-border>
                        The form has errors.
                    </w-alert>
                </div>
                <w-form
                    v-model="newNoteValid"
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
                         <w-textarea
                            label="Note Text" 
                            v-model="newNoteData.noteText"
                            :validators="[validators.required]"
                            >
                         </w-textarea>
                    </div>
                    <br />
                    <div>
                        <w-button type="submit">Submit Note</w-button>
                    </div>
                </w-form>
            </w-card>
        </w-flex>
    </div>
</template>

<script>
    export default {
        name:"NewAccountNote",
        props:{
            selectedAccountId:Number
        },
        emits:["newNoteClose","newNoteValidated"],
        data(){
            return {
                newNoteValid: null,
                newNoteData: { 
                    accountID:0,
                    noteText: '' 
                },
                validators: {
                    required: value => !!value || 'This field is required',
                },
            }
        },
        watch:{
            selectedAccountId:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.newNoteData.accountID = newVal;
                }
            }
        },
        methods:{
            validationSuccess(){
                this.$emit("newNoteValidated", this.newNoteData);
            },
            closeClick(){
                this.$emit("newNoteClose");
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>