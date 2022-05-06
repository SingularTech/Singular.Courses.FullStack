export class Phone {
  id!: number;
  brand!: string;
  model!: string;
  number!: string;

  constructor({ id, brand, model, number }: { id: number, brand: string, model: string, number: string }) {
    this.id = id;
    this.brand = brand;
    this.model = model;
    this.number = number;
  }
}
