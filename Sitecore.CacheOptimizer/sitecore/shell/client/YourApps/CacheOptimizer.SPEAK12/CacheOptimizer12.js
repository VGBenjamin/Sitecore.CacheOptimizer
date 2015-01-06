sitecore.component({
  name: "CacheOptimizer12",
  initialize: function (initial, app, el, sitecore) {
      this._super();
      this.ko = true;
      this.set("input", "");
      this.set("output", "");

      this.on("change:input", this.products, this);
  },

  sublayouts: function () {
      var input = this.get("input");

      $.ajax({
          url: "/api/sitecore/CacheOptimizer/GetSublayoutsList",
          type: "POST",
          data: { input: input },
          context: this,
          success: function (data) {
              this.set("output", data);
          }
      });
  }
});