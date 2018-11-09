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
									v-model="tag"
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
									<v-item v-for="chip in chips" :key="chip">
										<v-chip slot-scope="{ active, toggle }" close @input="remove(chip)"># {{chip}}</v-chip>
									</v-item>
								</v-item-group>
							</v-flex>
						</v-layout>
					</v-flex>
					<v-btn class="mt-4" color="success" block depressed>
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
      tag: '',
      chips: [
        'Programming',
        'Playing video games',
        'Watching movies',
        'Sleeping'
      ]
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
    addTag() {
      if (this.tag != '' && this.tag.length <= 20) this.chips.push(this.tag)
    },
    remove(item) {
      this.chips.splice(this.chips.indexOf(item), 1)
      this.chips = [...this.chips]
    }
  },
  mounted() {
    this.name = this.currentProject.name
    this.description = this.currentProject.description
    this.progress = this.currentProject.progress
    this.type = this.currentProject.type
    var a = document.getElementsByClassName('v-input__control')
    a[4].setAttribute('style', 'width:100%')
  },
  created() {}
}
</script>

