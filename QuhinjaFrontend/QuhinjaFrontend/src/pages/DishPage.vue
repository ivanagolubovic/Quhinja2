<template>
  <div class="row">
    <div class="col-8 q-pr-md q-pt-md">
      
        <div class="slika">
          <q-img style="height: 300px" :src="this.dish.picture" />
        </div>
        <div class="row name text-h6 ">
          <div class="col-6">
            {{ this.dish.name }}
          </div>
          <div class="col-6">{{ this.dish.dishType }} jelo</div>
        </div>
        <div class="row">
          <div v-if="!this.admin">
            <q-form @submit="onSubmit" class="q-gutter-md">
              <template v-if="this.dish.rate != 'Ne'" class="hidden1">
                <div class="text-red-1 col-8">
                  <q-rating
                    class="q-mb-md"
                    v-model="usersRatingFromBase"
                    max="5"
                    size="4em"
                    color="yellow"
                    icon="star_border"
                    icon-selected="star"
                    icon-half="star_half"
                    no-dimming
                  />
                  <q-item-label
                    class="text-h4 text-weight-medium q-pb-sm q-pl-sm q-mt-sm"
                    style="color: yellow"
                    >{{ this.dish.averageRating }}</q-item-label
                  >
                </div>
                <div class="col-4">
                  <q-btn
                    label="Oceni"
                    enable="false"
                    type="submit"
                    color="red-1"
                  />
                </div>
              </template>
            </q-form>
          </div>
        </div>
        <div>
          {{ this.dish.description }}
        </div>
     
    </div>
    <div class="col-4">
      
        <div
          class="row q-pt-md"
          v-for="recipe in this.dish.recipes"
          :key="recipe.id"
        >
          <div class="col-4 ">
            <q-img :src="recipe.picture"></q-img>
          </div>
          <div class="col-6 q-pl-lg text-h6 text-bold name">
            {{ recipe.name }}
          </div>
          <div class="col-2">
            <div class="q-gutter-sm q-pl-sm">
              <q-checkbox v-model="dish.id" />
            </div>
          </div>
        </div>
     
      <div v-for="el1 in user" :key="el1">
        <div v-for="el in comments" :key="el">
          <div class="q-pa-md name text-h8 text-weight-medium">
            Komentar postavio:
            <div class="text-h6">{{ el1 }}</div>
          </div>
          <div class="q-pa-md text-h7 name text-weight-medium">
            Komentar:
            <div class="text-h6">{{ el }}</div>
          </div>
        </div>
      </div>
      <div class="row full-width q-py-md">
        <q-input filled v-model="noviCom" label="Napisite komentar" />
        <div class="q-mt-lg q-pl-sm" @click="addComm">
          <q-img class="pic" src="../../public/add.svg" />
        </div>
      </div>
      <div class="row justify-around">
        <q-item
          clickable
          @click="handleAddRecipe"
          class="q-mb-md bg-red-2 itemForRecipe"
          style="
            width: 100%;
            display: flex;
            flex-direction: column;
            align-items: space-between;
            justify-content: center;
          "
        >
          <q-icon class="text-grey-4" size="20px" name="add"></q-icon>
        </q-item>
      </div>
      <div class="row" @click="deleteDish">
        <q-img class="pic" src="../../public/trash.svg" />
      </div>
          <div class="firstCard" @click="handleClickEdit">
      <h6>Edit</h6>
    </div>
    </div>
    <q-dialog
      v-model="visibleEditDishForm"
      persistent
      @hide="handleHideEditDishDialog"
    >
      <q-card class="q-py-sm full-width text-accent">
        <q-card-section
          class="q-ml-sm row full-width justify-between items-center"
        >
          <div class="text-h4 text-weight-medium name">Azuriranje jela</div>
          <q-btn
            icon="close"
            class="label"
            flat
            round
            dense
            @click="handleHideEditDishDialog"
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
                <div class="col-6">Novo ime jela</div>
                <div class="col-6">
                  <div>Nova slika jela</div>
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
                label="Azuriraj jelo"
                @click="editDish"
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
export default {
  data() {
    return {
      opis: "",
      ocena: "",
      dishType: "",
      name: "",
      image: "",
      visibleEditDishForm: false,
      noviCom: "",
      val: null,
      dish: null,
      color: "",
      usersRating: null,
      usersRatingFromBase: 0,
      showDetailsForRecipe: false,
      showRecipeList: true,
      recipeToShow: null,
      userData: {},
      admin: false,
      model: [],
      comments: [],
      user: [],
    };
  },
  methods: {
    handleHideEditDishDialog() {
      this.visibleEditDishForm = false;
    },
    handleClickEdit() {
      this.visibleEditDishForm = true;
    },
    editDish() {
      this.$store;
      this.$store
        .dispatch("apiRequest/putApiRequest", {
          url: `Dish/update-dish/${this.$route.params.id}`,
          data: {
            name: this.name,
            description: this.opis,
            picture: this.image,
            rate: this.ocena,
            dishType: this.dishType,
          },
          successMessage: "Uspesno ste promenili podatke",
          color: "brown",
        })
        .then((response) => {
          this.visibleEditDishForm = false;
          this.name = "";
          this.opis = "";
          this.image = "";
          this.ocena = "";
          this.dishType = "";
          this.getData();
        })
        .catch((response) =>
          this.$q.notify({
            position: "top",
            message: response.data,
            type: "negative",
          })
        );
    },
    deleteDish() {
      this.$store
        .dispatch("apiRequest/deleteApiRequest", {
          url: `Dish/${this.$route.params.id}`,
          successMessage: "Uspešno ste obrisali jelo",
        })
        .then((res) => {
          this.returnToMain();
        });
    },
    returnToMain() {
      this.$router.push("/dishes");
    },
    addComm() {
      const data = {
        dishId: this.dish.id,
        com: this.noviCom,
        userId: parseInt(this.userData.id),
      };
      this.$store
        .dispatch("apiRequest/postApiRequest", {
          url: "Dish/addComment",
          data: data,
          successMessage: "Uspešno ste dodali komentar",
        })
        .then((res) => {
          this.comments.push(this.noviCom);
          this.getDish();
        });
    },
    handleShowRecipe(recipe) {
      this.showDetailsForRecipe = true;
      this.showRecipeList = false;
      this.recipeToShow = recipe;
    },
    handleAddRecipe() {
      this.$router.push("/addRecipe/" + this.dish.id);
    },
    getDish() {
      this.$store
        .dispatch("apiRequest/getApiRequest", {
          url: `Dish/${this.$route.params.id}`,
        })
        .then((res) => {
          this.model = [];
          res.recipes.forEach((el) => {
            el.image = "data:image/png;base64," + el.image;
            if (el.id == res.selectedRecipeId) this.model.push(el.id);
          });
          console.log(this.model);
          this.dish = res;
          this.dish.image = "data:image/png;base64," + this.dish.image;
          this.getRating();
        });
    },
    getComments() {
      this.$store
        .dispatch("apiRequest/getApiRequest", {
          url: `Dish/getCommentsForDish/${this.$route.params.id}`,
        })
        .then((res) => {
          this.comments = [];
          this.comments = res;
        });
    },
    getUserForComments() {
      this.$store
        .dispatch("apiRequest/getApiRequest", {
          url: `Dish/getUsersOfCommentsForDish/${this.$route.params.id}`,
        })
        .then((res) => {
          this.user = [];
          this.user = res;
        });
    },
    onSubmit() {
      const data = {
        dishId: this.dish.id,
        rating: this.usersRatingFromBase,
        userId: parseInt(this.userData.id),
      };
      this.$store
        .dispatch("apiRequest/postApiRequest", {
          url: "Dish/rateDish",
          data: data,
          successMessage: "Uspešno ste ocenili jelo",
        })
        .then((res) => {
          this.getDish();
        });
    },
    getRating() {
      this.$store
        .dispatch("apiRequest/getApiRequest", {
          url: `user/getRatingForUser/${this.dish.id}`,
        })
        .then((res) => {
          this.usersRatingFromBase = res;
          console.log(res);
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
    checkBox(recipeId) {
      console.log(recipeId);
      const data = {
        selectedRecipeId: recipeId,
        id: this.dish.id,
      };
      this.$store
        .dispatch("apiRequest/postApiRequest", {
          url: "Dish/changeSelectedRecipe",
          data: data,
        })
        .then((res) => {
          this.getDish();
        });
    },
    check() {
      this.userData.roles.forEach((el) => {
        if (el == "admin") return (this.admin = true);
      });
    },
  },
  filters: {
    ParseFloat(number) {
      let newValue = parseFloat(number).toFixed(2);
      return newValue;
    },
  },
  created() {
    this.getDish();
    this.getUsersData();
    this.getComments();
    this.getUserForComments();
  },
};
</script>
<style lang="stylus" scoped>
.mainDiv {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.mainForRecipe {
  display: flex;
  flex-direction: column;
}



.topDiv {
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
}

.itemForRecipe {
  transition: 0.2s ease-in-out 0s;
}

.itemForRecipe:hover {
  cursor: pointer;
  color: white;
  transform: scale(1.1);
  border-style: solid;
  border-width: 4px;
  border-color: grey;
}

input[type=checkbox] {
  transform: scale(1.5);
}

input[type=checkbox] {
  width: 15px;
  height: 15px;
  margin-right: 8px;
  cursor: pointer;
  font-size: 10px;
  visibility: hidden;
}

input[type=checkbox]:after {
  content: ' ';
  background-color: white;
  display: inline-block;
  margin-left: 10px;
  padding-bottom: 5px;
  color: #baa671;
  width: 15px;
  height: 15px;
  visibility: visible;
  border: 1px solid #baa671;
  padding-left: 3px;
  border-radius: 5px;
}

input[type=checkbox]:checked:after {
  content: '\2714';
  padding: -5px;
  font-weight: bold;
}

.hidden1 {
  display: none;
}

.name {
  color: #5c5840;
}

.cards {
  justify-content: space-around;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  position: relative;
  width: 100%;
}

.pic {
  width: 20px;
  height: 20px;
}
</style>