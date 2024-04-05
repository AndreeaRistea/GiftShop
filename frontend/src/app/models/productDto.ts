export class ProductDto {
  Id: string = '';
  Name: string = '';
  Price: number = 0;
  Stock: number = 0;
  Description: string = '';
  ImageFile?: File;
  Image: string = '';
  CategoryId: string = '';
}
