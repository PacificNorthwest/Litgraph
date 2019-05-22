<template>
    <v-layout>
        <v-hover>
            <v-card slot-scope="{ hover }"
            :class="`material-card elevation-${hover ? 10 : 5}`"
            :style="`cursor: ${ hover ? 'pointer' : 'normal' }`">
                <v-card-title>
                    <div class="mt-3">
                        <h5 class="headline">{{ material.title }}</h5>
                        <div class="body-1">{{ material.brief }}</div>
                    </div>
                    
                </v-card-title>
            </v-card>
        </v-hover>
            
    </v-layout>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({
    name: 'material-card',
    props: ['material']    
})
</script>

<style lang="scss" scoped>
.material-card {
    height: 250px;
    flex-basis: 100%
}
</style>

