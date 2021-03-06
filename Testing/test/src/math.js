var x;

Test.assert(() => 10e3 == 10000, "Number exponent lower case");
Test.assert(() => 10E3 == 10000, "Number exponent upper case");

x = Math.min(4, 3);
Test.assert(() => x == 3, "Math.min 2 numbers - " + x);

x = Math.min(6, 4, 2, 8, 4);
Test.assert(() => x == 2, "Math.min 5 numbers");

x = Math.max(4, 3);
Test.assert(() => x == 4, "Math.max 2 numbers");

x = Math.max(6, 4, 2, 8, 4);
Test.assert(() => x == 8, "Math.max 5 numbers");

x = Math.floor(3.6);
Test.assert(() => x == 3, "Math.floor");

x = Math.ceil(3.4);
Test.assert(() => x == 4, "Math.ceil");

x = Math.abs(-5);
Test.assert(() => x == 5, "Math.abs");

x = Math.round(3.4);
Test.assert(() => x == 3, "Math.round down");

x = Math.round(3.7);
Test.assert(() => x == 4, "Math.round up");

x = Math.sqrt(64);
Test.assert(() => x == 8, "Math.sqrt");

x = Math.random();
Test.assert(() => x > 0 && x < 1, "Math.random");