<template>
  <q-page class="bg-white">
    <main v-if="admin" class="flexbox">
      <div class="flex col-6">
        <h5 class="label q-pt-lg"><u>Menu za dve nedelje</u></h5>

        <div class="flex column daysBox">
          <div
            class="flex row"
            style="justify-content: center; align-items: center; height: 85px"
            v-for="(day, index) in days.slice(
              (currentPage - 1) * daysPerPage,
              currentPage * daysPerPage
            )"
            :key="index"
          >
            <div
              :style="
                day.flag == false
                  ? 'background-color:white; color:#6f6e57;'
                  : ''
              "
              class="divForDate"
            >
              <p>
                {{ day.day | ParseDate }}
              </p>
              <p style="font-size: 10px">{{ day.day | ParseDate1 }}</p>
            </div>

            <div
              class="day"
              v-if="day.menuItem == null"
              :id="`${day.day}`"
              @dragover.prevent
              @drop.prevent="drop"
              bg-color="brown"
            ></div>
            <div class="boardMenu" v-if="day.menuItem != null">
              <Card
                class="q-mb-sm"
                :style="
                  day.flag2 == false
                    ? 'background-color:grey'
                    : 'background-color:white;'
                "
              >
                <q-img
                  width="65px"
                  :style="day.flag2 == false ? 'filter:grayscale(80%);' : ''"
                  style="height: 100%"
                  :src="day.menuItem.recipe.picture"
                ></q-img>
                <p class="text-red-1 q-ml-md" style="margin-auto">
                  {{ day.menuItem.recipe.dish.name }}
                </p>
              </Card>
              <div v-if="day.flag">
                <q-icon
                  name="delete"
                  class="deleteIcon text-red-1"
                  v-if="day.menuItem && day.flag2"
                  @click="deleteChild(`${day.day}`)"
                ></q-icon>
              </div>
            </div>
          </div>
          <div class="q-pa-lg flex flex-center">
            <q-pagination
              color="red-5"
              v-model="currentPage"
              :max="2"
              :max-pages="2"
              :boundary-numbers="true"
            >
            </q-pagination>
          </div>
        </div>
      </div>

      <div
        id="board-10"
        class="board col-6 column"
        @dragover.prevent
        @drop.prevent="drop"
      >
        <div class="label">
          <h5><u>Lista jela</u></h5>
        </div>

        <div class="bg-white q-ma-md q-ml-sm" style="width: 300px">
          <q-scroll-area style="height: 250px; max-width: 600px">
            <Card
              v-for="(dish, index) in dishes"
              :key="dish.id"
              :id="`${dish.selectedRecipeId}`"
              draggable="true"
            >
              <q-img
                width="65px"
                style="height: 100%"
                :src="dish.picture"
              ></q-img>

              <p class="text-red-1 q-ml-md">{{ dish.name }}</p>
            </Card>
          </q-scroll-area>
        </div>
        <div class="row q-pt-lg">
          <div class="col-1">
            <q-img class="pic q-pt-sm" src="../../public/add.svg" />
          </div>
          <div class="col-7">
            <div class="label text-h7">Dodaj novo jelo</div>
          </div>
          <div class="col-1">
            <q-img class="pic q-pt-sm" src="../../public/trash.svg" />
          </div>
          <div class="col-3">
            <div class="label text-h7">Obrisi</div>
          </div>
        </div>
        <div class="label q-pt-md text-bold text-h5">Posebni datumi</div>
        <div class="row q-pt-lg">
          <div class="col-1">
            <q-img class="pic q-pt-sm" src="../../public/cake.svg" />
          </div>
          <div class="col-5">
            <div class="label text-h7">Rodjendani</div>
          </div>
          <div class="col-1">
            <q-img class="pic q-pt-sm" src="../../public/anniversary.svg" />
          </div>
          <div class="col-5">
            <div class="label text-h7">Godisnjica u firmi</div>
          </div>
        </div>
      </div>
    </main>
    <main v-else class="row">
      <div class="row
                  justify-around
                  text-weight-medium text-h6
                  name
                  q-mt-lg">
        <div class="col-6 label text-h5">Trenutna nedelja</div>
        <div class="col-6 label text-h5">Naredna nedelja</div>
      </div>
      <div class="row
                  justify-around
                  text-weight-medium text-h6
                  name
                  q-mt-lg">
        <div class="col-6">
          <div
            class="flex row"
            style="justify-content: center; align-items: center; height: 85px"
            v-for="(day, index) in days.slice(
              (1 - 1) * daysPerPage,
              1 * daysPerPage
            )"
            :key="index"
          >
            <div
              :style="
                day.flag == false
                  ? 'background-color:white; color:#6f6e57;'
                  : ''
              "
              class="divForDate"
            >
              <p>
                {{ day.day | ParseDate }}
              </p>
              <p style="font-size: 10px">{{ day.day | ParseDate1 }}</p>
            </div>

            <div
              class="day"
              v-if="day.menuItem == null"
              :id="`${day.day}`"
              @dragover.prevent
              @drop.prevent="drop"
              bg-color="#5c5840"
            ></div>
            <div class="boardMenu" v-if="day.menuItem != null">
              <Card
                class="q-mb-sm"
                :style="
                  day.flag2 == false
                    ? 'background-color:color: #5c5840;'
                    : 'background-color:white;'
                "
              >
                <q-img
                  width="65px"
                  :style="day.flag2 == false ? 'filter:grayscale(80%);' : ''"
                  style="height: 100%"
                  :src="day.menuItem.recipe.picture"
                ></q-img>
                <p class="text-brown q-ml-md" style="margin-auto">
                  {{ day.menuItem.recipe.dish.name }}
                </p>
              </Card>
            </div>
          </div>
        </div>
        
        <div class="col-6">
          <div
            class="flex row"
            style="justify-content: center; align-items: center; height: 85px"
            v-for="(day, index) in days.slice(
              (2 - 1) * daysPerPage,
              2 * daysPerPage
            )"
            :key="index"
          >
            <div
              :style="
                day.flag == false
                  ? 'background-color:white; color:#6f6e57;'
                  : ''
              "
              class="divForDate"
            >
              <p>
                {{ day.day | ParseDate }}
              </p>
              <p style="font-size: 10px">{{ day.day | ParseDate1 }}</p>
            </div>

            <div
              class="day"
              v-if="day.menuItem == null"
              :id="`${day.day}`"
              @dragover.prevent
              @drop.prevent="drop"
              bg-color="brown"
            ></div>
            <div class="boardMenu" v-if="day.menuItem != null">
              <Card
                class="q-mb-sm"
                :style="
                  day.flag2 == false
                    ? 'background-color:#6f6e57'
                    : 'background-color:white;'
                "
              >
                <q-img
                  width="65px"
                  :style="day.flag2 == false ? 'filter:grayscale(80%);' : ''"
                  style="height: 100%"
                  :src="day.menuItem.recipe.picture"
                ></q-img>
                <p class="text-red-1 q-ml-md" style="margin-auto">
                  {{ day.menuItem.recipe.dish.name }}
                </p>
              </Card>
            </div>
          </div>
        </div>
      </div>
    </main>
  </q-page>
</template>
<script>
import moment from "moment";
import { date } from "quasar";
import Card from "./../components/Card.vue";
export default {
  components: {
    Card,
  },
  data() {
    return {
      days: [],
      dishes: [],
      dishesForView: [],
      status1: [],
      status2: [],
      admin: false,
      daysPerPage: 5,
      currentPage: 1,
      todayBirthUsers: [],
      todayEmployeeDateUsers: [],
    };
  },
  created() {
    this.getDishes();
    this.getUsersData();
    this.getDays();
    this.getTodayUsersData();
    this.getTodayEmplUsersData();
  },
  computed: {},
  filters: {
    ParseDate(date) {
      return (date = moment(date).format("D"));

      // put format as you want
    },
    ParseDate1(date) {
      return (date = moment(date).format("dddd"));
    },
    ParseDate2(date) {
      return (date = moment(date).format("LL"));
    },
  },

  methods: {
    getDays() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "MenuItem/twoWeeks" })
        .then((res) => {
          this.days = res;
          var x = new Date();

          var today = String(x.getDate()).padStart(2, "0");
          today = parseInt(today);
          var TodayM = String(x.getMonth() + 1).padStart(2, "0");
          TodayM = parseInt(TodayM);
          this.days.forEach((el) => {
            var date = moment(el.day).format("D");
            var month = moment(el.day).format("M");
            if (el.menuItem != null)
              el.menuItem.recipe.picture =
                "data:picture/png;base64," + el.menuItem.recipe.picture;
            // date = parseInt(date);
            if (today == date) el.flag = false;
            else el.flag = true;

            if (month < TodayM) {
              el.flag2 = false;
            } else if (month == TodayM) {
              if (today > date) el.flag2 = false;
              else el.flag2 = true;
            } else el.flag2 = true;
          });
          // var i = 0;
          // while (i < h.length) {
          //   if (h[i] != ",") h += h[i];
          //   i++;
          // }
        });
    },

    deleteChild(date) {
      const data = {
        dateOfDish: date,
      };
      this.$store
        .dispatch("apiRequest/postApiRequest", {
          url: "MenuItem/deleteByDate",
          data: data,
        })
        .then((res) => {
          this.getDays();
        });
    },

    drop(e) {
      const card_id = e.dataTransfer.getData("card_id");
      console.log(e.target.id);

      const card = document.getElementById(card_id);
      const newCard = card.cloneNode(true);

      var recipeId = card_id;
      var date = e.target.id;
      const data = {
        recipeId: parseInt(recipeId),
        dateOfDish: date,
      };
      this.$store
        .dispatch("apiRequest/postApiRequest", {
          url: "MenuItem/addMenuItem",
          data: data,
        })
        .then((res) => {
          this.getDays();
        });
    },

    getUsersData() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "user/0" })
        .then((res) => {
          this.userData = res;
          this.userData.image = "data:image/png;base64," + this.userData.image;

          this.check();
        });
    },

    getTodayUsersData() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "User/todayBirthUsers" })
        .then((res) => {
          this.todayBirthUsers = res;
          this.todayBirthUsers.forEach((element) => {
            element.image = "data:image/png;base64," + element.image;
          });
        });
    },
    getTodayEmplUsersData() {
      this.$store
        .dispatch("apiRequest/getApiRequest", { url: "User/todayEmplUsers" })
        .then((res) => {
          this.todayEmployeeDateUsers = res;
          this.todayEmployeeDateUsers.forEach((element) => {
            element.image = "data:image/png;base64," + element.image;
          });
        });
    },
    check() {
      this.userData.roles.forEach((el) => {
        if (el == "admin") return (this.admin = true);
      });
    },
    getDishes() {
      this.$store.dispatch("apiRequest/getApiRequest", { url: "Dish" }).then(
        (res) => (
          (this.dishes = res.filter((dish) => dish.selectedRecipe != null)),
          this.dishes.forEach((element) => {
            element.image = "data:image/png;base64," + element.image;
          }),
          (this.dishesForView = this.dishes)
        )
      );
    },
  },
};
</script>
<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
.daysBox {
  width: 100%;
  justify-content: center;
  margin-right: 100px;
}
@media screen and (max-height: 1000px) {
  .day {
    display: flex;
    flex-direction: column;
    width: 100%;
    max-width: 300px;
    height: 65px;
    align-items: center;
    justify-content: center;
    padding: 15px;
    background-color: #6f6e57;
  }
}
@media screen and (max-height: 800px) {
  .pic {
    width: 19px;
    height: 19px;
  }
  .picc {
    height: 100px;
  }
  .label {
    color: #5c5840;
  }
  .day {
    display: flex;
    flex-direction: column;
    width: 100%;
    max-width: 300px;
    padding: 15px;
    height: 65px;
  }
}
h5 {
  font-family: "Open Sans";
}
.flexbox {
  display: flex;
  width: 100%;
  max-width: 900px;
  height: 100vh;
  margin: 0 auto;
  padding: 15px;
}
.flexbox .board {
  display: flex;
  flex-direction: column;
  width: 100%;
  max-width: 300px;
  padding: 15px;
}
.flexbox .boardMenu {
  display: flex;
  flex-direction: row;
  align-items: start;
  justify-content: start;
  width: 100%;
  max-width: 300px;
  padding: 15px;
}
.divForDate {
  width: 65px;
  height: 65px;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-right: 20px;
  border: solid #6f6e57;
  font-size: 30px;
}
.flexbox .board .card {
  background-color: white;
  cursor: pointer;
  margin-bottom: 15px;
  border: solid #6f6e57;
  transition: 0.2s ease-in-out 0s;

  font-size: 20px;
  height: 65px;
  width: 100%;
  align-items: center;
}
.flexbox .board .card:hover {
  transform: scale(0.95);
}
.flexbox .boardMenu .card {
  cursor: pointer;
  align-items: center;
  font-size: 20px;
  height: 65px;
  border: solid #6f6e57;
  width: 100%;
}
.deleteIcon {
  font-size: 25px;
  transition: 0.2s ease-in-out 0s;
}
.deleteIcon:hover {
  cursor: pointer;
  transform: scale(1.2);
  color: #000;
}
</style>
