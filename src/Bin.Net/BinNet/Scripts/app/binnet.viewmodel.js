window.binnetApp = window.binnetApp || {};

window.binnetApp.binnetViewModel = (function(ko) {
    /// <field name="todoLists" value="[new datacontext.todoList()]"></field>
    var binValue = ko.observableArray([1, 2, 3]),
        error = ko.observable(),
        changeValue = function(appender, value) {
            binValue(value);
        },
        byteToHex = function(byteValue) {
            return ("0" + (Number(byteValue).toString(16))).slice(-2).toUpperCase();
        },
        hexToByte = function(hexString) {
            return parseInt(hexString, 16);
        },
        basicValue = ko.computed(
            {
                read: function() {
                    var bin = binValue();
                    var result = "";
                    for (var i = 0; i < bin.length; i++) {
                        result += byteToHex(bin[i]) + " ";
                    }
                    return result;
                },
                write: function(value) {
                    var result = [];
                    while (value.length >= 2) {
                        while (value[0] == ' ') {
                            value = value.slice(1);
                        }
                        result.push(hexToByte(value.substring(0, 2)));
                        value = value.substring(2, value.length);
                    }

                    binValue(result);
                },
                owner: this
            }),
        compactValue = ko.computed(
            {
                read: function() {
                    var bin = binValue();
                    var result = "";
                    for (var i = 0; i < bin.length; i++) {
                        result += byteToHex(bin[i]);
                    }
                    return result;
                },
                write: function(value) {
                    var result = [];
                    while (value.length >= 2) {
                        while (value[0] == ' ') {
                            value = value.slice(1);
                        }
                        result.push(hexToByte(value.substring(0, 2)));
                        value = value.substring(2, value.length);
                    }

                    binValue(result);
                },
                owner: this
            }),
        cStyleValue = ko.computed(function() {
            var bin = binValue();
            var result = "byte[" + bin.length + "] { ";
            for (var i = 0; i < bin.length; i++) {
                result += "0x" + byteToHex(bin[i]) + ", ";
            }
            return result + "}";
        }),
        signedDecValue = ko.computed(function() {
            var bin = binValue();
            var result = "[ ";
            for (var i = 0; i < bin.length; i++) {
                result += i > 127 ? i - 256 : i;
                result += ", ";
            }

            result = result.trim();
            return result + "]";
        }),
        oppositeValue = ko.computed({
            read: function () {
                var bin = binValue();
                var result = "";
                for (var i = 0; i < bin.length; i++) {
                    var oppositeByte = bin[i] > 0 ? 256 - bin[i] : 0;
                    result += byteToHex(oppositeByte) + " ";
                }
                return result;
            },
            write: function (value) {
                var result = [];
                while (value.length >= 2) {
                    while (value[0] == ' ') {
                        value = value.slice(1);
                    }
                    var oppositeByte = hexToByte(value.substring(0, 2));
                    result.push(oppositeByte == 0 ? 0 : 256 - oppositeByte);
                    value = value.substring(2, value.length);
                }

                binValue(result);
            },
            owner: this
        });


    //datacontext.getTodoLists(todoLists, error); // load todoLists

    return {
        binValue: binValue,
        error: error,
        basicValue: basicValue,
        compactValue: compactValue,
        cStyleValue: cStyleValue,
        signedDecValue: signedDecValue,
        oppositeValue: oppositeValue
    };

})(ko);

// Initiate the Knockout bindings
ko.applyBindings(window.binnetApp.binnetViewModel);