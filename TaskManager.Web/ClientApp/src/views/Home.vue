<template>
	<v-container grid-list-xs class="pr-0 pl-0">
		<v-layout row wrap="" class="pr-4 pl-4 mb-4" v-if="$vuetify.breakpoint.mdAndUp">
			<v-flex xs12>
				<v-form lazy-validation class="text-xs-right">
					<v-text-field
						v-model="name"
						label="Имя проекта"
						prepend-icon="add"
						clearable
						counter="80"
						required
						@input="$v.name.$touch()"
						@blur="$v.name.$touch()"
						@keyup.enter="onSubmit"
					></v-text-field>
					<v-btn
						:loading="loading"
						color="primary"
						class="mr-0"
						depressed
						:disabled="$v.$invalid"
						@click="onSubmit"
					>Создать</v-btn>
				</v-form>
			</v-flex>
		</v-layout>
		<v-divider v-if="$vuetify.breakpoint.mdAndUp"></v-divider>
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
								<v-icon v-html="typesIcon[props.item.type]"></v-icon>
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
		<v-container grid-list-xl class="pb-5" v-if="$vuetify.breakpoint.smAndDown">
			<v-layout row wrap="" justify-end>
				<v-flex xs12 class="text-xs-right">
					<v-bottom-sheet v-model="sheet">
						<v-btn
							slot="activator"
							fixed
							bottom
							right
							color="primary"
							dark
							fab
							style="margin-bottom: 60px"
						>
							<v-icon dark>add</v-icon>
						</v-btn>
						<v-list>
							<v-container grid-list-xs>
								<v-layout row wrap="">
									<v-flex xs12>
										<v-form lazy-validation class="text-xs-right">
											<v-text-field
												v-model="name"
												label="Имя проекта"
												prepend-icon="add"
												clearable
												required
												@input="$v.name.$touch()"
												@blur="$v.name.$touch()"
												@keyup.enter="onSubmit"
											></v-text-field>
											<v-btn
												:loading="loading"
												color="primary"
												class="mr-0"
												depressed
												:disabled="$v.$invalid"
												@click="onSubmit"
											>Создать</v-btn>
										</v-form>
									</v-flex>
								</v-layout>
							</v-container>
						</v-list>
					</v-bottom-sheet>
				</v-flex>
			</v-layout>
		</v-container>
	</v-container>
</template>

<script>
import { required, maxLength } from 'vuelidate/lib/validators'
import { mapGetters, mapActions } from 'vuex'
export default {
  validations: {
    name: { required, maxLength: maxLength(80) }
  },
  data() {
    return {
      name: '',
      rowsPerPageItems: [3, 6, 12],
      pagination: {
        rowsPerPage: 3
      },
      sheet: false,
      search: '',
      typesIcon: ['add', 'person', 'info', 'list']
    }
  },
  computed: {
    ...mapGetters(['projects', 'loading']),
    filteredProjects() {
      return this.projects.filter(project =>
        project.name.toLowerCase().match(this.search)
      )
    }
  },
  methods: {
    ...mapActions(['addProject', 'setCurrentProject']),
    setProject(item) {
      this.setCurrentProject(item)
      this.$router.push('/project')
    },
    onSubmit() {
      const project = {
        name: this.name,
        description:
          'Lorem ipsum dolor sit amet consectetur adipisicing elit. Hic, error obcaecati modi facere blanditiis reprehenderit quis?',
        icon: 'add'
      }
      this.addProject(project)
      this.name = ''
      this.sheet = false
    }
  }
}
</script>

