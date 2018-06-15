// Copyright 2009 the Sputnik authors.  All rights reserved.
// This code is governed by the BSD license found in the LICENSE file.

/*---
info: If B = 10xxxxxx or B = 11111xxx, throw URIError
es5id: 15.1.3.2_A1.3_T1
es6id: 18.2.6.3
esid: sec-decodeuricomponent-encodeduricomponent
description: Complex tests. B = 10xxxxxx -> B in [0x80 - 0xBF]
includes: [decimalToHexString.js]
---*/

var errorCount = 0;
var count = 0;
var indexP;
var indexO = 0;

for (var index = 0x80; index <= 0xBF; index++) {
  count++;
  var hex = decimalToPercentHexString(index);
  try {
    decodeURIComponent(hex);
  } catch (e) {
    if ((e instanceof URIError) === true) continue;
  }
  if (indexO === 0) {
    indexO = index;
  } else {
    if ((index - indexP) !== 1) {
      if ((indexP - indexO) !== 0) {
        var hexP = decimalToHexString(indexP);
        var hexO = decimalToHexString(indexO);
        $ERROR('#' + hexO + '-' + hexP + ' ');
      }
      else {
        var hexP = decimalToHexString(indexP);
        $ERROR('#' + hexP + ' ');
      }
      indexO = index;
    }
  }
  indexP = index;
  errorCount++;
}

if (errorCount > 0) {
  if ((indexP - indexO) !== 0) {
    var hexP = decimalToHexString(indexP);
    var hexO = decimalToHexString(indexO);
    $ERROR('#' + hexO + '-' + hexP + ' ');
  } else {
    var hexP = decimalToHexString(indexP);
    $ERROR('#' + hexP + ' ');
  }
  $ERROR('Total error: ' + errorCount + ' bad Unicode character in ' + count + ' ');
}
