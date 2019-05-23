<template v-if="ranking.length">
  <v-container>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
          <template>
            <div class="text-xs-center">
              <v-btn v-on:click="fetchListings(true)" :loading="loading" class="white--text" color="#0071b3">Show with garden</v-btn>
              <v-btn v-on:click="fetchListings()" :loading="loading2" class="white--text" color="#0071b3">Show without garden</v-btn>
            </div>
          </template>
      </v-flex>
      <v-flex xs12 results>
        <v-data-table :headers="headers" :items="ranking" hide-actions>
          <template slot="items" slot-scope="props">
            <td class="text-xs-center">{{ props.index + 1}}</td>
            <td class="text-xs-left">{{ props.item.name }}</td>
            <td class="text-xs-center">{{ props.item.listingCount }}</td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      ranking: [],
      errors: [],
      headers: [
        {
          text: "Rank",
          value: "index",
          sortable: false,
          align: "center"
        },
        {
          text: "Real Estate Agent",
          align: "left",
          sortable: false,
          value: "name"
        },
        {
          text: "# Objects",
          value: "listingCount",
          sortable: false,
          align: "center"
        }
      ],
      loading: false,
      loading2: false
    };
  },
  methods: {
    fetchListings(filter) {
      if (filter) {
        this.loading = true;

        axios
          .get(`api/ranking`)
          .then(response => {
            this.loading = false;
            this.ranking = response.data;
          })
          .catch(e => {
            this.errors.push(e);
          });
      } else {
        this.loading2 = true;

        axios
          .get(`api/ranking?filter=`)
          .then(response => {
            this.loading2 = false;            
            this.ranking = response.data;
          })
          .catch(e => {
            this.errors.push(e);
          });
      }
    }
  }
};
</script>

<style>
  .results {
    margin-top: 1.55rem;
  }
</style>