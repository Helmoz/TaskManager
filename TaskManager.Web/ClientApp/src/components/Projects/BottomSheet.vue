<template>
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
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import { required, maxLength } from 'vuelidate/lib/validators'
export default {
  validations: {
    name: { required, maxLength: maxLength(80) }
  },
  data() {
    return {
      sheet: false,
      name: ''
    }
  },
  computed: {
    ...mapGetters(['loading'])
  },
  methods: {
    ...mapActions(['addProject']),
    onSubmit() {
      const project = {
        name: this.name,
        description:
          'Lorem ipsum dolor sit amet consectetur adipisicing elit. Hic, error obcaecati modi facere blanditiis reprehenderit quis?',
        icon: 'add'
      }
      this.addProject(project)
      this.sheet = false
    }
  }
}
</script>

