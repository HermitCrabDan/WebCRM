<template>
    <div>
        <w-app block>
            <h1>Members</h1>
            <br />
            <w-notification v-model="isLoading" success bg-color="white">
                ....loading
            </w-notification>
            <w-notification v-model="isError" error bg-color="white">
                An Error Occured
            </w-notification>
            <div v-if="newMode">
                <w-flex justify-center fill-width>
                </w-flex>
            </div>
            <div v-else-if="editMode">
                <w-flex justify-center fill-width>
                </w-flex>
            </div>
            <div v-else>
            </div>
        </w-app>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name:"Members",
    data(){
        return {
            MemeberList: [],
            MemberListHeaders: [
                { label:'Member ID', key:'id', align:'left' },
                { label:'Member Name', key:'memberName', align:'left' },
                { label:'User ID', key:'userID', align:'left' },
                { label:'Created by', key:'createdBy', align:'left' },
                { label:'Date Entered', key:'creationDateString', align:'left' },
                { label:'Updated by', key:'lastUpdatedBy', align:'left' },
                { label:'Last Updated', key:'lastUpdatedDateString', align:'left' },
            ],
            isError: false,
            isLoading: false,
            newMode: false,
            editMode: false,
            apiUrl: "api/MemberData",
            selectedMember: {},
            editFormValid: null,
            keyword:'',
            keywordFilter: keyword => item => {
            // Concatenate all the columns into a single string for a more performant lookup.
            const allTheColumns = `${item.accountName} ${item.createdBy} ${item.lastUpdatedBy}`

            // Lookup the keyword variable in the string with case-insensitive flag.
            return new RegExp(keyword, 'i').test(allTheColumns)
            },
            validators: {
                required: value => !!value || 'This field is required',
            },
        }
    },
    methods:{
        getList(){
            this.isLoading = true;
            this.isError = false;
            axios
                .get(this.apiUrl)
                .then(response => { 
                    this.MemeberList = response.data; 
                })
                .catch(error => {
                    console.log(error);
                    this.isError = true;
                })
                .then(() => {
                    this.isLoading = false;
                });
        }
    }
}
</script>

<style lang="scss" scoped>

</style>