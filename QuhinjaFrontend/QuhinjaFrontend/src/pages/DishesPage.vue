<template>
  <div class="q-pa-md row items-start q-gutter-md">
    <div class="cards">
      <div v-if="!this.admin" class="hidden1">
        <q-card>
          <div class="firstCard">
            <q-img src="../../public/add.svg" />
          </div>
        </q-card>
      </div>
      <div v-else class="row items-center addCard">
        <div class="col-12 q-pa-xl">
          <div
            class="col-12 q-pa-md"
            style="display: flex; flex-direction: row; justify-content: center"
          >
            <div class="firstCard" @click="handleClickPlus">
              <q-img src="../../public/add.svg" />
            </div>
          </div>
          <div class="col-4 q-tm-md">
            <label class="text-h6 name">Dodajte novo jelo</label>
          </div>
          <div class="Cup q-pt-xl">
            <q-img src="../../public/dodajNovoJelo.png" />
          </div>
        </div>
      </div>
      <q-card
        v-for="(dish, index) in dishesForView.slice(
          (currentPage - 1) * dishesPerPage,
          currentPage * dishesPerPage
        )"
        :key="index"
        class="my-card"
      >
        <div class="col-4">
          <q-img :src="dish.picture" class="picc" />
          <div class="q-pa-md">
            <div class="col text-h6 name ellipsis">
              {{ dish.name }}
            </div>
          </div>
          <div class="row q-pa-md">
            <q-rating
              v-model="dish.averageRating"
              size="2em"
              :max="5"
              color="yellow"
              readonly
            />
            <div class="q-mx-sm"></div>
            <div class="text-h6 text-yellow justify-content:flex-end">
              {{ dish.averageRating }}
            </div>
          </div>
          <div
            style="
              text-overflow: ellipsis;
              white-space: nowrap;
              overflow: hidden;
              height: 130px;
            "
            class="q-px-md text-subtitle1 name"
          >
            {{ dish.description }}
          </div>
        </div>
        <div class="q-pl-xl q-ml-lg"></div>
        <div
          class="name text-subtitle2"
          style="position: absolute; right: 10px; bottom: 1px"
          @click="handleClick(dish.id)"
          flat
        >
          See more >>
        </div>
      </q-card>
    </div>
    <q-dialog
      v-model="visibleAddDishForm"
      persistent
      @hide="handleHideAddDishDialog"
    >
      <q-card class="q-py-sm full-width text-accent">
        <q-card-section
          class="q-ml-sm row full-width justify-between items-center"
        >
          <div class="text-h4 text-weight-medium name">
            Dodavanje novog jela
          </div>
          <q-btn
            icon="close"
            class="text-brown-5"
            flat
            round
            dense
            @click="handleHideAddDishDialog"
          />
        </q-card-section>
        <q-card-section class="q-pt-none">
          <q-form ref="form" class="full-width column q-gutter-y-md">
            <div class="name forma">
              <q-separator></q-separator>
              <div
                class="
                  row
                  justify-around
                  text-weight-medium text-h6
                  name
                  q-mt-lg
                "
                style="height: 35px"
              >
                <div class="col-6">Ime jela</div>
                <div class="col-6">
                  <div>Ubaci sliku</div>
                </div>
              </div>
              <div
                class="
                  row
                  justify-around
                  text-weight-medium text-h6
                  name
                  q-pt-md
                "
              >
                <div class="col-6 q-pr-sm">
                  <q-input filled v-model="name" label="Npr. Pizza" />
                </div>
                <div class="col-6">
                  <q-input filled v-model="image" label="Upload image" />
                </div>
              </div>
              <div
                class="
                  row
                  justify-around
                  text-weight-medium text-h6
                  name
                  q-mt-lg
                "
                style="height: 35px"
              >
                <div class="col-6">Vreme pripreme</div>
                <div class="col-6">
                  <div>Link za recept</div>
                </div>
              </div>
              <div class="row text-h8 name q-py-md">
                <div class="col-6">
                  <div class="row justify-between">
                    <div class="col-4 q-pr-sm">
                      <q-item-label>h</q-item-label>
                      <q-input v-model.number="sati" type="number" filled />
                    </div>
                    <div class="col-4 q-pr-sm">
                      <q-item-label>min</q-item-label>
                      <q-input v-model.number="min" type="number" filled />
                    </div>
                    <div class="col-4 q-pr-sm">
                      <q-item-label>sek</q-item-label>
                      <q-input v-model.number="sek" type="number" filled />
                    </div>
                  </div>
                </div>
                <div class="col-6 q-pt-md">
                  <q-input v-model="link" filled />
                </div>
              </div>
              <div class="row text-h6 name">
                <div class="col-6">Dozvoli ocenjivanje jela</div>
                <div class="col-6">Da li je jelo ljuto?</div>
              </div>
              <div class="row justify-start text-h6 name">
                <div class="col-6">
                  <q-radio
                    color="brown-7"
                    class="q-pr-sm"
                    dense
                    v-model="ocena"
                    val="Da"
                    label="Da"
                  />
                  <q-radio
                    color="brown-7"
                    dense
                    v-model="ocena"
                    val="Ne"
                    label="Ne"
                  />
                </div>
                <div class="col-6">
                  <q-radio
                    class="q-pr-sm"
                    color="brown-7"
                    dense
                    v-model="dishType"
                    val="Ljuto"
                    label="Da"
                  />
                  <q-radio
                    color="brown-7"
                    dense
                    v-model="dishType"
                    val="Neljuto"
                    label="Ne"
                  />
                </div>
              </div>
              <label class="text-h6 name q-pt-md">Sastojci</label>
              <div class="row text-h6 name">
                <div v-for="(el, index) in nizSastojaka" :key="el">
                  <div class="row justify-around q-pa-sm">
                    <q-btn
                      align="around"
                      class="btn-fixed-width"
                      text-color="brown-8"
                      color="grey-2"
                      @click="removeElement(index)"
                      >{{ el }}
                      <div class="q-pb-md q-pl-md">x</div>
                    </q-btn>
                  </div>
                </div>
              </div>
              <div class="row justify-between text-h6 name q-py-md">
                <div class="col-4 q-pr-sm">
                  <q-input filled v-model="imeSastojka" label="Sastojak" />
                </div>
                <div class="col-3 q-pr-sm">
                  <q-input filled v-model="kolicina" label="Kolicina " />
                </div>
                <div class="col-4 q-pr-sm">
                  <q-input
                    filled
                    v-model="mernaJedinica"
                    label="Merna jedinica"
                  />
                </div>
                <div class="q-pt-sm q-pl-sm col-1" @click="addsastojak">
                  <q-img class="pic" src="../../public/add.svg" />
                </div>
              </div>
              <label class="text-h6 name q-py-sm">Opis jela</label>
              <div class="q-pt-md">
                <q-input
                  v-model="opis"
                  filled
                  type="textarea"
                  label="Napisi nesto o jelu..."
                />
              </div>
            </div>
            <q-card-actions align="right" class="bg-white text-teal">
              <q-btn
                class="backBtn"
                flat
                label="Dodaj jelo i recept"
                @click="addDish"
                v-close-popup
              />
            </q-card-actions>
          </q-form>
        </q-card-section>
      </q-card>
    </q-dialog>
  </div>
</template>
<script>

import { baseUrl } from "../services/apiConfig";
import { formRulesMixin } from "src/helper/formRulesMixin";
export default {
  mixins: [formRulesMixin],
  data() {
    return {
      idDish: 0,
      idRecept: 0,
      image: "",
      dishType: "Ljuto",
      ocena: "Da",
      model: null,
      ratingModel: 0,
      visibleAddDishForm: false,
      dishes: [],
      sastojci: [],
      merneJedinice: [],
      kolicine: [],
      kolicina: "",
      ocenjivanje: true,
      mernaJedinica: "",
      nizSastojaka: [],
      sortingOptions: [],
      sortingOption: "",
      dishesForView: [],
      search: "",
      dishesPerPage: 8,
      usersRating: null,
      currentPage: 1,
      admin: false,
      userData: {},
      sortBool: false,
      imeSastojka: "",
      name: "",
      opis: "",
      link: "",
      min: 0,
      sati: 0,
      sek: 0,
      vreme: " ",
    };
  },
  computed: {
    numOfDishes() {
      return this.dishesForView.length;
    },
    numOfPages() {
      if (this.numOfDishes % this.dishesPerPage == 0)
        return this.numOfDishes / this.dishesPerPage;
      else {
        return this.numOfDishes / this.dishesPerPage + 1;
      }
    },
  },
  watch: {
    search: function () {
      if (this.search == "") {
        this.dishesForView = this.dishes;
      } else {
        this.dishesForView = this.dishesForView.filter((dish) => {
          return dish.name.toLowerCase().startsWith(this.search.toLowerCase());
        });
      }
    },
  },
  filters: {
    ParseFloat(number) {
      let newValue = parseFloat(number).toFixed(2);
      return newValue;
    },
  },
  methods: {
    handleClick(id) {
      this.$router.push("dish/" + id);
    },
    addDish() {
      const dataDish = {
        name: this.name,
        description: this.opis,
        picture: this.image,
        rate: this.ocena,
        dishType: this.dishType,
      };
      console.log(dataDish);
      this.$store
        .dispatch("apiRequest/postApiRequest", {
          url: "dish",
          data: dataDish,
          successMessage: "Uspešno ste dodali jelo",
        })
        .then((res) => {
          this.idDish = res;
          this.addRecept();
        });
      this.getData();
    },
    addRecept() {
      const dataRecept = {
        name: this.name,
        dishId: this.idDish,
        WayOfPreparing: "",
        preview: this.link,
        preparationTime: this.vratiVreme(),
      };
      console.log(dataRecept);
      this.$store
        .dispatch("apiRequest/postApiRequest", {
          url: "recipe",
          data: dataRecept,
          successMessage: "Uspešno ste dodali recept",
        })
        .then((res) => {
          this.idRecept = res;
          this.addIngridents();
        });
      console.log(this.idRecept);
      console.log(dataRecept);
    },
     addIngridents() {
      var i;
      for (i = 0; i < this.sastojci.length; i++) {
        const data = {
          recipeId: this.idRecept,
          quantity: parseInt(this.kolicine[i]),
          ingridient: {
            name: this.sastojci[i],
          },
          unit: this.merneJedinice[i],
        };
        console.log(this.sastojci[i]),
        console.log(this.merneJedinice[i]),
        console.log(this.kolicine[i]),
        
        
        this.$store.dispatch("apiRequest/postApiRequest", {
          url: "ingridient/addIngridient",
          data: data,
           successMessage: "Uspešno ste dodali sastojke"
        });
      }
    },
    vratiVreme() {
      this.vreme = this.sati + "h " + this.min + "min " + this.sek + "s";
    },
    addsastojak() {
      this.sastojci.push(this.imeSastojka);
      this.kolicine.push(this.kolicina);
      this.merneJedinice.push(this.mernaJedinica);
      this.nizSastojaka.push(
        this.imeSastojka + " " + this.kolicina + " " + this.mernaJedinica
      );
    },
    removeElement: function (index) {
      this.nizSastojaka.splice(index, 1);
      this.sastojci.splice(index, 1);
      this.kolicine.splice(index, 1);
      this.merneJedinice.splice(index, 1);
    },
    handleClickPlus() {
      this.visibleAddDishForm = true;
    },
    handleHideAddDishDialog() {
      this.visibleAddDishForm = false;
    },
    sortDishes(option) {
      this.sortBool = true;
      this.dishesForView = [];
      this.dishes.forEach((element) => {
        if (element.dishType == option) this.dishesForView.push(element);
      });
    },
    getData() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "Dish" })
        .then((res) => {
          this.dishes = res;
          this.dishes.forEach((element) => {
            element.image = "data:image/png;base64," + element.image;
          });
          this.dishesForView = this.dishes;
        });
    },
    getDishTypes() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: `/dish/dishTypes` })
        .then((res) => {
          this.sortingOptions = res;
        });
    },
    getUsersData() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "user/0" })
        .then((res) => {
          this.userData = res;
          this.check();
        });
    },
    check() {
      this.userData.roles.forEach((el) => {
        if (el == "admin") return (this.admin = true);
      });
    },
  },
  created() {
    this.getData();
    this.getDishTypes();
    this.getUsersData();
  },
};
</script>
<style scoped>
.cards {
  justify-content: center;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  position: relative;
}
.my-card,
.addCard {
  width: 260px;
  height: 505px;
  margin: 59px 20px 31px;
  padding: 0 0 29px;
  background-color: #fbfbfb;
  display: flex;
  flex-direction: column;
}
.name {
  color: #5c5840;
}
.hidden1 {
  display: none;
}
.firstCard {
  width: 29.3px;
  height: 29.3px;
  background-color: #fbfbfb;
  display: flex;
}
.cup {
  width: 185px;
  height: 230px;
}
.cardClass {
  display: flex;
  flex-direction: column;
  justify-items: stretch;
}
.forma {
  justify-content: center;
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  position: relative;
}
.pic {
  width: 19.5px;
  height: 19.5px;
}
.backBtn {
  background-color: #5c5840;
  color: white;
}
.picc {
  width: 100%;
  height: 240px;
}
</style>