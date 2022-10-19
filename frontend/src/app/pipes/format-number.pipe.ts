import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatNumber'
})
export class FormatNumberPipe implements PipeTransform {

  transform(
    value: number,
    minFractions: number = 0,
    maxFractions: number = 0,
    thousandSeparator: string = " ",
    decimalSeparator: string = "."
  ): string {
    var numStr = value.toLocaleString("en", {
      minimumFractionDigits: minFractions,
      maximumFractionDigits: maxFractions
    });

    numStr = numStr.replace(",", "<T>");
    numStr = numStr.replace(".", "<D>");

    numStr = numStr.replace("<T>", thousandSeparator);
    numStr = numStr.replace("<D>", decimalSeparator);

    return numStr;
  }

}
