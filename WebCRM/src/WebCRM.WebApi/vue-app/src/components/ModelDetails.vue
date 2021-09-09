<template>
    <div>
        <div style="text-align:left; border:solid 1px silver; padding:5px; min-width:400px">
            <div style="text-align:center">
                <h3>Details</h3>
            </div>
            <hr />
            <div>
                Id: {{ modelData.id }}
            </div>
            <div>
                Last Updated: {{ modelData.lastUpdatedDateString }}
            </div>
            <div>
                Updated By: {{ modelData.lastUpdatedBy }}
            </div>
            <div>
                Deleted: {{ modelData.deletionDateString }}
            </div>
            <div>
                Deleted By: {{ modelData.deletionBy }}
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name:"ModelDetails",
        props:{
            modelData: Object
        }
    }
</script>

<style lang="scss" scoped>

</style>