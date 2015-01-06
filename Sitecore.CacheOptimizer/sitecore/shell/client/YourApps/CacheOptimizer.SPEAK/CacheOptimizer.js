define(["sitecore"], function (Sitecore) {
  var model = Sitecore.Definitions.App.extend({
      initialized: function (options) {

        this.set("input", "");
        this.set("output", "");

        this.on("change:input", this.sublayouts, this);
        this.sublayouts();
      },

    sublayouts: function() {
        var input = this.get("input");

        $.ajax({
            url: "/api/sitecore/CacheOptimizer/GetSublayoutsList",
            type: "POST",
            data: { input: input },
            context: this,
            success: function(data) {
                this.set("output", data);
            }
        });
    }
  });
    return model;
});