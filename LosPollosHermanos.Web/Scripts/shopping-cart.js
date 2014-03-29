function formatCurrency(value) {
    return value.toFixed(2) + '€';
}

var CartLine = function () {
    var self = this;
    self.product = ko.observable();
    self.quantity = ko.observable(1);
    self.subtotal = ko.computed(function () {
        return self.product() ? self.product().Price * parseInt("0" + self.quantity(), 10) : 0;
    });
};

var Cart = function () {
    // Stores an array of lines, and from these, can work out the grandTotal
    var self = this;
    self.lines = ko.observableArray([new CartLine()]); // Put one line in by default
    self.grandTotal = ko.computed(function () {
        var total = 0;
        $.each(self.lines(), function() { total += this.subtotal(); });
        return total;
    });
    
    // available products
    self.products = ko.observableArray([]);

    // Operations
    self.addLine = function () { self.lines.push(new CartLine()); };
    self.removeLine = function (line) { self.lines.remove(line); };
    self.save = function () {
        var dataToSave = $.map(self.lines(), function (line) {
            return line.product() ? {
                ProductId: line.product().Id,
                Quantity: line.quantity()
            } : undefined;
        });
        
        $.ajax({
            type: 'POST',
            url: '/Home/AddOrder',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(dataToSave),
            dataType: 'json',
            cache: false
        })
           .done(function (data) {
               if (data) {
                   alert('Ordered!');
                   document.location.href = '/Home/Order';
                   //self.lines([new CartLine()]);
               }
           })
           .fail(function (data) {
               alert('fail ordering');
           });
    };
    self.reload = function () {
        $.ajax({
            type: 'POST',
            url: '/Home/Products',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false
        })
            .done(function (data) {
                self.products(data);
            })
            .fail(function (data) {
                alert('fail getting data');
            });
    };

    self.reload();
};

ko.applyBindings(new Cart());