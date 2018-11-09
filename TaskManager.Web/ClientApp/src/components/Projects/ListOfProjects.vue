<template>
	<v-container>
		<v-text-field v-model="search" placeholder="Search" append-icon="search" solo></v-text-field>
		<v-data-iterator
			:items="filteredProjects"
			:rows-per-page-items="rowsPerPageItems"
			rows-per-page-text="Элементов на странице:"
			:pagination.sync="pagination"
			content-tag="v-layout"
			row
			wrap=""
			:loading="loading"
		>
			<v-flex slot="item" slot-scope="props" xs12>
				<v-list two-line>
					<v-list-tile @click ripple>
						<v-list-tile-avatar>
							<v-icon v-html="typeIcons[props.item.type]"></v-icon>
						</v-list-tile-avatar>
						<v-list-tile-content>
							<v-list-tile-title>{{ props.item.name }}</v-list-tile-title>
							<v-list-tile-sub-title>{{props.item.description}}</v-list-tile-sub-title>
						</v-list-tile-content>
						<v-list-tile-action>
							<v-progress-circular
								:rotate="360"
								:size="40"
								:width="5"
								:value="props.item.progress"
								color="teal"
							>
								<small>{{ props.item.progress }}</small>
							</v-progress-circular>
						</v-list-tile-action>
						<v-list-tile-action>
							<v-btn icon ripple @click.prevent="setProject(props.item)">
								<v-icon color="grey lighten-1">info</v-icon>
							</v-btn>
						</v-list-tile-action>
					</v-list-tile>
				</v-list>
			</v-flex>
		</v-data-iterator>
	</v-container>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'
export default {
  data() {
    return {
      rowsPerPageItems: [3, 6, 12],
      pagination: {
        rowsPerPage: 3
      },
      search: ''
    }
  },
  computed: {
    ...mapGetters(['projects', 'loading', 'typeIcons']),
    filteredProjects() {
      return this.projects.filter(project =>
        project.name.toLowerCase().match(this.search)
      )
    }
  },
  methods: {
    ...mapActions(['setCurrentProject']),
    setProject(item) {
      this.setCurrentProject(item)
      this.$router.push('/project')
    }
  }
}
</script>

