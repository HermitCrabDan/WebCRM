<template>
    <div>
        <w-flex>
            <div style="text-align:left">
                <w-checkbox
                    v-model="showDeleted"
                    @input="updateViewableData">
                    Show Deleted Items
                </w-checkbox>
            </div>
            <br />
            <div>
                <w-input 
                    v-model="keyword"
                    placeholder="Search any column..."
                    inner-icon-left="wi-search"
                    class="mb3"
                    style="text-align:left"
                    >
                </w-input>
                <w-table
                    :headers="headerDataList"
                    :items="viewableData"
                    :selectable-rows="1"
                    :loading="isLoading"
                    :filter="keywordFilter(keyword)"
                    @row-select="selectDataItem($event.item)">
                </w-table>
            </div>
        </w-flex>
    </div>
</template>

<script>
    export default {
        name:"ModelListBase",
        props:{
            dataList:Array,
            headerList:Array,
            sortColumn:String,
        },
        emits:["dataItemSelected"],
        data(){
            return{
                listData:[],
                viewableData:[],
                headerDataList:[],
                dataSort:'',

                showDeleted:false,
                
                keyword:'',
                keywordFilter: keyword => item => {
                    // Concatenate all the columns into a single string for a more performant lookup.
                    const allTheColumns = `${item.transactionAmountString} ${item.transactionDateString} ${item.createdBy} ${item.lastUpdatedBy}`
                    // Lookup the keyword variable in the string with case-insensitive flag.
                    return new RegExp(keyword, 'i').test(allTheColumns)
                },
            }
        },
        methods:{
            updateViewableData(){
                this.viewableData = [];
            },
            selectDataItem(selectedData){
                this.$emit("dataItemSelected", selectedData);
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>