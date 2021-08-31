<template>
    <div>
        <select v-model="SelectedMember">
            <option disabled value="">Please select a member</option>
            <option v-for="member in MemberList" :key="member.Id" :value="member.Id">
                {{ member.MemberName }}
            </option>
        </select>
    </div>
</template>

<script>
    export default {
        name:"AddAccountMembership",
        emits: ["AddMemberToAccount"],
        props:{
            Account: Object,
            MemeberList: Array,
        },
        data(){
            return {
                selectedMemberId: '',
                selectedMember: null,
                isLoading: false,
                isError: false
            }
        },
        methods:{
            updateSelectedAccount(){
                if (this.MemeberList && this.selectedMemberId != ''){
                    this.MemeberList.forEach(element => {
                        if (element.Id == this.selectedMemberId){
                            this.selectedMember = element;
                        }
                    });
                }
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>