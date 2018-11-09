<template>
	<v-container class="pb-5">
		<v-layout row wrap="">
			<v-flex xs12>
				<v-layout row wrap="">
					<v-flex xs6>
						<v-text-field
							v-model="name"
							label="Имя проекта"
							counter="80"
							required
							:error-messages="nameErrors"
							@input="$v.name.$touch()"
							@blur="$v.name.$touch()"
						></v-text-field>
					</v-flex>
					<v-flex xs12>
						<v-textarea label="Описание" v-model="description" counter="300"></v-textarea>
					</v-flex>
					<v-flex xs12>
						<v-layout align-end justify-space-between>
							<v-flex xs6>
								<v-subheader class="pl-0">Выполнено на {{progress}}%</v-subheader>
								<v-slider v-model="progress" class="" thumb-label></v-slider>
							</v-flex>
							<v-flex xs5>
								<v-subheader class="pl-0">Дедлайн</v-subheader>
								<v-menu
									:close-on-content-click="false"
									v-model="datePicker"
									:nudge-right="40"
									lazy
									transition="scale-transition"
									offset-y
									full-width
									min-width="290px"
								>
									<v-text-field slot="activator" v-model="deadline" prepend-icon="event" class="" readonly></v-text-field>
									<v-date-picker v-model="deadline" @input="datePicker = false"></v-date-picker>
								</v-menu>
							</v-flex>
						</v-layout>
					</v-flex>
					<v-flex xs12>
						<v-layout row wrap="">
							<v-flex xs12 sm8>
								<v-subheader class="pl-0">Тип проекта</v-subheader>
								<v-layout row wrap="">
									<v-flex xs2 class="">
										<v-icon large v-html="typeIcons[type]"></v-icon>
									</v-flex>
									<v-flex xs9 sm7>
										<v-select
											:items="types"
											v-model="type"
											item-value="type"
											item-text="name"
											class="pt-0 mt-0"
										></v-select>
									</v-flex>
								</v-layout>
							</v-flex>
						</v-layout>
					</v-flex>
					<v-flex xs12>
						<v-layout row wrap="">
							<v-flex xs12 sm4>
								<v-text-field
									v-model="tag.name"
									label="Добавить тэг"
									append-icon="add"
									@click:append="addTag"
									@keyup.enter="addTag"
									prefix="#"
									counter="20"
									required
									@input="$v.tag.$touch()"
									@blur="$v.tag.$touch()"
									:error-messages="tagErrors"
								></v-text-field>
							</v-flex>
							<v-flex xs12 sm7 class="ml-2">
								<v-item-group multiple>
									<v-item v-for="tag in tags" :key="tag.name">
										<v-chip slot-scope="{ active, toggle }" close @input="remove(tag)"># {{tag.name}}</v-chip>
									</v-item>
								</v-item-group>
							</v-flex>
						</v-layout>
					</v-flex>
					<v-btn class="mt-4" color="success" block depressed @click="saveChanges()">
						<v-icon class="mr-2">save</v-icon>Сохранить изменения
					</v-btn>
				</v-layout>
			</v-flex>
		</v-layout>
	</v-container>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import { required, maxLength } from 'vuelidate/lib/validators'
export default {
  validations: {
    tag: { required, maxLength: maxLength(20) },
    name: { required, maxLength: maxLength(80) }
  },
  data() {
    return {
      name: '',
      description: '',
      progress: null,
      deadline: null,
      datePicker: false,
      type: null,
      types: [
        { name: 'Идея', type: 0 },
        { name: 'В разработке', type: 1 },
        { name: 'Завершен', type: 2 }
      ],
      tag: { name: '' },
      tags: []
    }
  },
  computed: {
    ...mapGetters(['currentProject', 'typeIcons']),
    tagErrors() {
      const errors = []
      if (!this.$v.tag.$dirty) return errors
      !this.$v.tag.maxLength && errors.push('Слишком длинный тэг')
      !this.$v.tag.required && errors.push('Введите тэг')
      return errors
    },
    nameErrors() {
      const errors = []
      if (!this.$v.name.$dirty) return errors
      !this.$v.name.maxLength && errors.push('Слишком длинное имя')
      !this.$v.name.required &&
        errors.push('Это поле обязательно к заполнению.')
      return errors
    }
  },
  methods: {
    ...mapActions(['editProject']),
    addTag() {
      if (this.tag.name != '' && this.tag.name.length <= 20)
        this.tags.push({ name: this.tag.name })
    },
    remove(item) {
      this.tags.splice(this.tags.indexOf(item), 1)
      this.tags = [...this.tags]
    },
    saveChanges() {
      var date = new Date(this.deadline)
      date.setHours(10, 10, 10)
      const project = {
        id: this.currentProject.id,
        name: this.name,
        description: this.description,
        progress: this.progress,
        deadline: date,
        type: this.type,
        tags: this.tags
      }
      this.editProject(project)
    }
  },
  mounted() {
    this.name = this.currentProject.name
    this.description = this.currentProject.description
    this.progress = this.currentProject.progress
    this.type = this.currentProject.type
    this.deadline = new Date(this.currentProject.deadline)
      .toISOString()
      .substr(0, 10)
    this.tags = this.currentProject.tags
  },
  created() {}
}
</script>

