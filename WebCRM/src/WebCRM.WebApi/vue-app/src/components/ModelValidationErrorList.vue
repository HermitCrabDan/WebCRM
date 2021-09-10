<template>
    <div>
        <div>
            <w-list
                :items="errorDataList"
                color="red"
                icon="wi-cross"
                >
            </w-list>
        </div>
    </div>
</template>

<script>
    export default {
        name:"ModelValidationErrorList",
        props:{
            errorList: Array
        },
        data(){
            return{
                errorDataList:[],
            }
        },
        watch:{
            errorList:{
                immediate:true,
                deep:true,
                handler(newVal){
                    this.errorDataList = newVal.map(x =>  { 
                        var labelData = { label: x };
                        return labelData; 
                    });
                        //this.memberIdList = this.crmAccountMemberList.map(x => x.memberID);;
                    console.log(this.errorDataList);
                }
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>